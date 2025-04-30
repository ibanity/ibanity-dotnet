using System.Collections.Generic;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Errors collection.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Errors list.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }
    }

    /// <summary>
    /// Error.
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error description.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Error source.
        /// </summary>
        public ErrorSource Source { get; set; }
    }

    /// <summary>
    /// Error source.
    /// </summary>
    public class ErrorSource
    {
        /// <summary>
        /// Error pointer.
        /// </summary>
#pragma warning disable CA1720
        public string Pointer { get; set; }
#pragma warning restore CA1720
    }
}
