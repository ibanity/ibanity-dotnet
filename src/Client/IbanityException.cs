using System;

namespace Ibanity.Apis.Client
{
    public class IbanityException : ApplicationException
    {
        public IbanityException(string message) : base(message) { }
    }

    public class IbanityConfigurationException : IbanityException
    {
        public IbanityConfigurationException(string message) : base(message) { }
    }
}
