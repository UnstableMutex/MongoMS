using System;

namespace MongoMS
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class HeaderAttribute : Attribute
    {
        private readonly string _header;

        // This is a positional argument
        public HeaderAttribute(string header)
        {
            _header = header;
        }

        public string Header
        {
            get { return _header; }
        }
    }
}