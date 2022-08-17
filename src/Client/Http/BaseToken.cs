namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Base access token, only containing access token value.
    /// </summary>
    public class BaseToken
    {
        /// <summary>
        /// Bearer token.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
