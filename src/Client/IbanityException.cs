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

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="message">Error description</param>
        /// <param name="innerException">Original error</param>
        public IbanityException(string message, Exception innerException) : base(message, innerException) { }
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
