using System.Collections.Generic;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PaginatedCollection<T> : List<T>
    {
        public PaginatedCollection(IEnumerable<T> v) : base(v) { }

        public ContinuationToken ContinuationToken { get; set; }
    }

    public class ContinuationToken
    {
        internal ContinuationToken(string nextUrl) =>
            NextUrl = nextUrl;

        internal string NextUrl { get; }
    }
}