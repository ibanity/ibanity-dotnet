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
    }
}
