namespace JwtNet
{
    public class JwtNetOptions
    {
        public required ISigningStrategy SigningStrategy { get; set; }



        private string? _issuer;

        /// <summary>
        /// Indicates whether the issuer of the token should be validated.
        /// </summary>
        /// <remarks>
        /// This property is automatically set to <see langword="true"/> if <see cref="Issuer"/> has a non-empty value.
        /// </remarks>
        public bool ValidateIssuer { get; private set; }

        /// <summary>
        /// Issuer of the token.
        /// </summary>
        /// <remarks>
        /// Setting this property will automatically update <see cref="ValidateIssuer"/>.
        /// </remarks>
        public string? Issuer
        {
            get => _issuer;
            set
            {
                _issuer = value;
                ValidateIssuer = !string.IsNullOrEmpty(_issuer);
            }
        }



        private string? _audience;

        /// <summary>
        /// Indicates whether the audience of the token should be validated.
        /// </summary>
        /// <remarks>
        /// This property is automatically set to <see langword="true"/> if <see cref="Audience"/> has a non-empty value.
        /// </remarks>
        public bool ValidateAudience { get; private set; }

        /// <summary>
        /// The intended audience of the token.
        /// </summary>
        /// <remarks>
        /// Setting this property will automatically update <see cref="ValidateAudience"/>.
        /// </remarks>
        public string? Audience
        {
            get => _audience;
            set
            {
                _audience = value;
                ValidateAudience = !string.IsNullOrEmpty(_audience);
            }
        }



        private int _expiresInMinutes = 20;

        /// <summary>
        /// Indicates whether the lifetime of the token should be validated.
        /// </summary>
        /// <remarks>
        /// This property is automatically set to <see langword="true"/> if <see cref="ExpiresInMinutes"/> has a value greater than 0.
        /// </remarks>
        public bool ValidateLifetime { get; private set; } = false;

        /// <summary>
        /// The expiration time in minutes.
        /// </summary>
        /// <remarks>
        /// Defaults to 20 minutes. Cannot be set to less than 0.
        /// </remarks>
        public int ExpiresInMinutes
        {
            get => _expiresInMinutes;
            set {
                _expiresInMinutes = value > 0 ? value : 0;
                ValidateLifetime = value > 0;
            }
        }



        /// <summary>
        /// The allowed clock skew for token validation.
        /// </summary>
        /// <remarks>
        /// This property is useful for handling minor time discrepancies between the server and clients. Defaults to 5 minutes.
        /// </remarks>
        public TimeSpan ClockSkew { get; set; }

    }
}
