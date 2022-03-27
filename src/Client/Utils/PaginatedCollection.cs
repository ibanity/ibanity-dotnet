using System;
using System.Collections.Generic;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PaginatedCollection<T> : List<T>
    {
        public PaginatedCollection(IEnumerable<T> collection) :
            base(collection ?? throw new ArgumentNullException(nameof(collection)))
        { }

        public ContinuationToken ContinuationToken { get; set; }
    }

    public class ContinuationToken
    {
        internal ContinuationToken(string nextUrl) =>
            NextUrl = nextUrl;

        internal string NextUrl { get; }
    }
}
