using System;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Access token, generated from an authorization code or a refresh token.
    /// </summary>
    public class Token
    {
        private string _refreshToken;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        public Token() { }

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="accessToken">Bearer token</param>
        /// <param name="validUntil">Validity limit</param>
        /// <param name="refreshToken">Token used to get a new bearer token</param>
        /// <exception cref="ArgumentException"></exception>
        public Token(string accessToken, DateTimeOffset validUntil, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or whitespace.", nameof(refreshToken));

            AccessToken = accessToken;
            ValidUntil = validUntil;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Bearer token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Validity limit.
        /// </summary>
        public DateTimeOffset ValidUntil { get; set; }

        /// <summary>
        /// Token used to get a new bearer token.
        /// </summary>
        /// <remarks>Don't forget to save this value if you want to reuse the token later.</remarks>
        public string RefreshToken
        {
            get => _refreshToken;
            set
            {
                var previousValue = _refreshToken;
                _refreshToken = value;

                if (_refreshToken == previousValue)
                    return;

                var handler = RefreshTokenUpdated;
                if (handler != null)
                    handler(this, new RefreshTokenUpdatedEventArgs { PreviousToken = previousValue, NewToken = _refreshToken });
            }
        }

        /// <summary>
        /// The refresh token was nearly expired and was replaced by a new one.
        /// </summary>
        public event EventHandler<RefreshTokenUpdatedEventArgs> RefreshTokenUpdated;
    }

    /// <summary>
    /// Former and current refresh tokens.
    /// </summary>
    public class RefreshTokenUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Former refresh token
        /// </summary>
        public string PreviousToken { get; set; }

        /// <summary>
        /// Current refresh token
        /// </summary>
        public string NewToken { get; set; }
    }
}
