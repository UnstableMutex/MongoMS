using System.Text.RegularExpressions;

namespace MVVMLight.Extras
{
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
    public class Int32Validator : Validator
    {
       
        public Int32Validator(string message)
            : base(message)
        {
           
        }
        public override bool Validate(object value)
        {
            var s = value.ToString();
            int a;
            return int.TryParse(s, out a);
        }
    }
}