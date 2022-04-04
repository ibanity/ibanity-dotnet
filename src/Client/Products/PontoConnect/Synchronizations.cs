using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Synchronizations : ResourceClient<Synchronization, object, object, object>, ISynchronizations
    {
        private const string EntityName = "synchronizations";

        public Synchronizations(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<Synchronization> Create(Token token, SynchronizationRequest synchronization, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (synchronization is null)
                throw new ArgumentNullException(nameof(synchronization));

            var payload = new JsonApi.Data<SynchronizationRequest, object, object, object>();
            payload.Type = "synchronization";
            payload.Attributes = synchronization;

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        public Task<Synchronization> Get(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);
    }

    public interface ISynchronizations
    {
        Task<Synchronization> Create(Token token, SynchronizationRequest synchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
        Task<Synchronization> Get(Token token, Guid id, CancellationToken? cancellationToken = null);
    }
}
