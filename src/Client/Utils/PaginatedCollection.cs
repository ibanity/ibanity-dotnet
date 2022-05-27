using System;
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
    /// <para>List of resources.</para>
    /// <para>Contains a token to get the next page.</para>
    /// </summary>
    /// <typeparam name="T">Resource type.</typeparam>
#pragma warning disable CA1711 // Keep 'Collection' name
    public class IbanityCollection<T> : PaginatedCollection<T>
#pragma warning restore CA1711
    {
        /// <summary>
        /// Maximum number (1-100) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 10.
        /// </summary>
        public long? PageLimit { get; set; }

        /// <summary>
        /// Cursor that specifies the first resource of the next page.
        /// </summary>
        public Guid? BeforeCursor { get; set; }

        /// <summary>
        /// Cursor that specifies the last resource of the previous page.
        /// </summary>
        public Guid? AfterCursor { get; set; }

        /// <summary>
        /// Link to the first page.
        /// </summary>
        public string FirstLink { get; set; }

        /// <summary>
        /// Link to the previous page.
        /// </summary>
        public string PreviousLink { get; set; }

        /// <summary>
        /// Link to the next page.
        /// </summary>
        public string NextLink { get; set; }
    }

    /// <summary>
    /// <para>List of resources.</para>
    /// <para>Contains a token to get the next page.</para>
    /// </summary>
    /// <typeparam name="T">Resource type.</typeparam>
#pragma warning disable CA1711 // Keep 'Collection' name
    public class IsabelCollection<T> : PaginatedCollection<T>
#pragma warning restore CA1711
    {
    }

    /// <summary>
    /// Token allowing to fetch the next page of a <see cref="IbanityCollection&lt;T&gt;" /> or a <see cref="IsabelCollection&lt;T&gt;" />.
    /// </summary>
    public class ContinuationToken
    {
        internal ContinuationToken(string nextUrl) =>
            NextUrl = nextUrl;

        internal string NextUrl { get; }
    }
}
