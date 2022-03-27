using System.Collections.Generic;

namespace Ibanity.Apis.Client.JsonApi
{
    public class Error
    {
        public List<ErrorItem> Errors { get; set; }
    }

    public class ErrorItem
    {
        public string Code { get; set; }
        public string Detail { get; set; }
    }
}
