using System;
using System.Collections.Generic;

namespace Ibanity.Apis.Client.Utils
{
    /// <summary>
    /// <para>List of resources.</para>
    /// <para>Contains a token to get the next page.</para>
    /// </summary>
    /// <typeparam name="T">Resource type.</typeparam>
    public class PaginatedCollection<T> : List<T>
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="collection">Contained resources.</param>
        public PaginatedCollection(IEnumerable<T> collection) :
            base(collection ?? throw new ArgumentNullException(nameof(collection)))
        { }

        /// <summary>
        /// Token allowing to fetch the next page.
        /// </summary>
        public ContinuationToken ContinuationToken { get; set; }
    }

    /// <summary>
    /// Token allowing to fetch the next page of a <see cref="PaginatedCollection&lt;T&gt;" />.
    /// </summary>
    public class ContinuationToken
    {
        internal ContinuationToken(string nextUrl) =>
            NextUrl = nextUrl;

        internal string NextUrl { get; }
    }
}
