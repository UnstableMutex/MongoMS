using System;

namespace MongoMS.Common
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class WindowCommandAttribute : Attribute
    {
        private readonly string _name;

        public WindowCommandAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsDefault { get; set; }
    }
}