using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</para>
    /// <para>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect</see>.</para>
    /// </summary>
    public class BulkPaymentInitiationRequests : ResourceClient<BulkPaymentInitiationRequest, object, object, object, string, Token>, IBulkPaymentInitiationRequests
    {
        private const string EntityName = "bulk-payment-initiation-requests";

        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider<Token> _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BulkPaymentInitiationRequests(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public Task<BulkPaymentInitiationRequest> Get(Token token, string id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public async Task<BulkPaymentInitiationRequest> Create(Token token, string filename, string path, bool? isShared = null, bool? hideDetails = null, CancellationToken? cancellationToken = null)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return await Create(token, filename, stream, isShared, hideDetails, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BulkPaymentInitiationRequest> Create(Token token, string filename, Stream xmlContent, bool? isShared = null, bool? hideDetails = null, CancellationToken? cancellationToken = null)
        {
            var headers = new Dictionary<string, string>();

            if (isShared.HasValue)
                headers.Add("Is-Shared", isShared.Value.ToString().ToLowerInvariant());

            if (hideDetails.HasValue)
                headers.Add("Hide-Details", hideDetails.Value.ToString().ToLowerInvariant());

            var result = await _apiClient.PostInline<JsonApi.Resource<BulkPaymentInitiationRequest, object, object, object>>(
                $"{_urlPrefix}/{EntityName}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token))).ConfigureAwait(false)).AccessToken,
                headers,
                filename,
                xmlContent,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            return Map(result.Data);
        }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</para>
    /// <para>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect</see>.</para>
    /// </summary>
    public interface IBulkPaymentInitiationRequests
    {
        /// <summary>
        /// Get Bulk Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Bulk Payment Initiation Request ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a bulk payment initiation request resource</returns>
        Task<BulkPaymentInitiationRequest> Get(Token token, string id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Bulk Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filename">As shown to the user in Isabel 6</param>
        /// <param name="path">Local path the the XML file to upload</param>
        /// <param name="isShared">Defines if the payment file can be accessed by the other users having the right mandate on Isabel 6. Defaults to <c>true</c>.</param>
        /// <param name="hideDetails">Defines if the details (on single transactions) within the payment file can be viewed by other users on Isabel 6 or not. Defaults to <c>false</c>.</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a bulk payment initiation request resource</returns>
        Task<BulkPaymentInitiationRequest> Create(Token token, string filename, string path, bool? isShared = null, bool? hideDetails = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Bulk Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filename">As shown to the user in Isabel 6</param>
        /// <param name="xmlContent">XML content to upload</param>
        /// <param name="isShared">Defines if the payment file can be accessed by the other users having the right mandate on Isabel 6. Defaults to <c>true</c>.</param>
        /// <param name="hideDetails">Defines if the details (on single transactions) within the payment file can be viewed by other users on Isabel 6 or not. Defaults to <c>false</c>.</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a bulk payment initiation request resource</returns>
        Task<BulkPaymentInitiationRequest> Create(Token token, string filename, Stream xmlContent, bool? isShared = null, bool? hideDetails = null, CancellationToken? cancellationToken = null);
    }
}
