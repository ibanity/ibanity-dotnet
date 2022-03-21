using System.Text;

namespace Ibanity.Apis.Client.Crypto
{
    public class RsaSsaPssSignature : ISignature
    {
        public string Sign(string value)
        {
            return string.Empty;
        }
    }

    public interface ISignature
    {
        string Sign(string value);
    }
}
