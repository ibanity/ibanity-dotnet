using System.Collections.Generic;

namespace Ibanity.Apis.Client.Utils
{
    /// <summary>
    /// <para>List of resources.</para>
    /// <para>Contains a token to get the next page.</para>
    /// </summary>
    /// <typeparam name="T">Resource type.</typeparam>
#pragma warning disable CA1711 // Keep 'Collection' name
    public class PaginatedCollection<T>
#pragma warning restore CA1711
    {
        /// <summary>
        /// Token allowing to fetch the next page.
        /// </summary>
        public ContinuationToken ContinuationToken { get; set; }

        /// <summary>
        /// Actual list.
        /// </summary>
        public List<T> Items { get; set; }
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
