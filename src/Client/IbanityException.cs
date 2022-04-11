using System;

namespace Ibanity.Apis.Client
{
    /// <summary>
    /// Base exception for all Ibanity errors.
    /// </summary>
    public class IbanityException : ApplicationException
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="message">Error description</param>
        public IbanityException(string message) : base(message) { }
    }

    /// <summary>
    /// Something is misconfigured.
    /// </summary>
    public class IbanityConfigurationException : IbanityException
    {
        /// <inheritdoc />
        public IbanityConfigurationException(string message) : base(message) { }
    }
}
