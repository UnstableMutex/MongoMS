using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using NLog;

namespace MVVMLight.Extras
{
    public abstract class VMB : ViewModelBase
    {


        protected virtual void RealSave()
        {
        }

        //protected virtual bool IsPropertyIgnoredOnSave(string propertyName)
        //{
        //    return false;
        //}

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected virtual void SaveQuery(string propertyName)
        {

            RealSave();
            logger.Debug("RealSave Executed");

        }

        protected void RaisePropertyChangedNoSave([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
        }
        protected sealed override void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
            SaveQuery(propertyName);
        }

        protected sealed override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged(propertyExpression);
        }

        protected sealed override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
        }

        protected sealed override void RaisePropertyChanged<T>([CallerMemberName]string propertyName = null, T oldValue = default(T), T newValue = default(T),
            bool broadcast = false)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
        }

        protected void AssignCommands<T>()
        {
            var t = GetType();
            var commands = t.GetProperties().Where(p => p.PropertyType == typeof(ICommand) && p.Name.EndsWith("Command"));
            foreach (var propertyInfo in commands)
            {
                TryToAssign<T>(t, propertyInfo);
            }
        }
        private void TryToAssign<T>(Type type, PropertyInfo propertyInfo)
        {
            var methodname = propertyInfo.Name.RemoveEnd("Command");
            var canmethodname = "Can" + methodname;
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var m = type.GetMethod(methodname, flags, null, Type.EmptyTypes, null);
            var canm = type.GetMethod(canmethodname, flags);
            var cmdtype = typeof(T);
            if (m != null)
            {
                if (canm == null)
                {
                    var ctor = cmdtype.GetConstructor(new[] { typeof(Action) });
                    var cmd = (ICommand)ctor.Invoke(new object[] { (Action)(() => m.Invoke(this, null)) });
                    propertyInfo.SetValue(this, cmd);
                }
                else
                {
                    var ctor = cmdtype.GetConstructor(new[] { typeof(Action), typeof(Func<bool>) });
                    var cmd = (ICommand)ctor.Invoke(new object[] { (Action)(() => m.Invoke(this, null)), (Func<bool>)(() => (bool)canm.Invoke(this, null)) });
                    propertyInfo.SetValue(this, cmd);
                }
            }
        }
    }


    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class NoWeakRelayCommand<T> : ICommand
    {
        #region Constructors

        public NoWeakRelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public NoWeakRelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion // ICommand Members

        #region Fields

        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;

        #endregion // Fields
    }

    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class NoWeakRelayCommand : ICommand
    {
        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public NoWeakRelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public NoWeakRelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public virtual void Execute(object parameter)
        {
            _execute();
        }

        #endregion // ICommand Members

        #region Fields

        readonly Action _execute;
        readonly Func<bool> _canExecute;

        #endregion // Fields
    }
    public class VMBValidated : VMB, INotifyDataErrorInfo
    {



        protected sealed override void SaveQuery(string propertyName)
        {
            Validate(propertyName);
            if (!HasErrors)
            {
                base.SaveQuery(propertyName);
            }
        }



        #region validating
        protected bool Validate(string propertyName)
        {

            // ReSharper disable once AssignNullToNotNullAttribute
            var pi = GetType().GetProperty(propertyName);
            var valuetovalidate = pi.GetValue(this);
            var customattr = pi.GetCustomAttributes(true);
            var validators = customattr.OfType<Validator>();

            var errorList = new List<string>();
            foreach (var validator in validators)
            {
                bool b = validator.Validate(valuetovalidate);
                if (!b)
                {
                    errorList.Add(validator.Message);
                }
            }
            if (errorList.Count == 0)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                if (_errorsDictionary.ContainsKey(propertyName))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    _errorsDictionary.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);

                }
                return true;
            }
            else
            {

                // ReSharper disable once AssignNullToNotNullAttribute
                if (!_errorsDictionary.ContainsKey(propertyName))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    _errorsDictionary[propertyName] = new VError();
                    _errorsDictionary[propertyName].Errors.AddRange(errorList);
                    RaiseErrorsChanged(propertyName);
                }
                else
                {
                    var curerrors = string.Join("", _errorsDictionary[propertyName]);
                    var newerrors = string.Join("", errorList);
                    if (curerrors != newerrors)
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        _errorsDictionary[propertyName].Errors.Clear();
                        _errorsDictionary[propertyName].Errors.AddRange(errorList);
                        RaiseErrorsChanged(propertyName);
                    }
                }
                return false;
            }
        }

        readonly Dictionary<string, VError> _errorsDictionary = new Dictionary<string, VError>();
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errorsDictionary.ContainsKey(propertyName))
                return _errorsDictionary[propertyName];
            else
                return null;
        }
        public bool HasErrors
        {
            get { return _errorsDictionary.Count != 0; }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            var e = new DataErrorsChangedEventArgs(propertyName);
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, e);
            }
        }

        #endregion


    }

    internal class VError:IEnumerable
    {
        public List<string> Errors { get; set; }
        public IEnumerator GetEnumerator()
        {
            return Errors.GetEnumerator();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class Validator : Attribute
    {
        private readonly string _message;

        protected Validator(string message)
        {
            _message = message;
        }
        public string Message
        {
            get { return _message; }
        }
        public abstract bool Validate(object value);
    }
    public class RegexValidator : Validator
    {
        private readonly string _re;
        public RegexValidator(string re, string message)
            : base(message)
        {
            _re = re;
        }
        public override bool Validate(object value)
        {
            return Regex.IsMatch(value.ToString(), _re, RegexOptions.IgnoreCase);
        }
    }
    public static class ext
    {
        public static string RemoveEnd(this string s, string end, StringComparison c = StringComparison.CurrentCultureIgnoreCase)
        {
            var len = end.Length;
            if (s.EndsWith(end, c))
            {
                return s.Substring(0, s.Length - len);
            }
            else
            {
                throw new WrongStringEndException();
            }
        }
        public static string RemoveEndIfExists(this string s, string end, StringComparison c = StringComparison.CurrentCultureIgnoreCase)
        {
            var len = end.Length;
            if (s.EndsWith(end, c))
            {
                return s.Substring(0, s.Length - len);
            }
            else
            {
                return s;
            }
        }
    }
    public class WrongStringEndException : Exception
    {
    }
    public class MyRelayCommand : ICommand
    {
        #region Fields
        readonly Action _execute;
        readonly Func<bool> _canExecute;
        #endregion // Fields
        #region Constructors
        public MyRelayCommand(Action execute)
            : this(execute, null)
        {
        }
        public MyRelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors
        #region ICommand Members
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            _execute();
        }
        #endregion // ICommand Members
    }
}
