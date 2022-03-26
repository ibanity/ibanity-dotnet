using System.Threading;
using System.Threading.Tasks;

namespace Ibanity.Apis.Client.Http
{
    public interface ITokenProvider : IBearerTokenProvider, IRefreshTokenProvider { }

    public interface IBearerTokenProvider
    {
        Task<string> GetBearerToken(CancellationToken cancellationToken);
    }

    public interface IRefreshTokenProvider
    {
        Task<string> GetRefreshToken(CancellationToken cancellationToken);
    }
}
