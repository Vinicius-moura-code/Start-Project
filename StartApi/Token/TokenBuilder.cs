using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StartApi.Token
{
    public class TokenBuilder
    {
        private SecurityKey? securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private readonly Dictionary<string, string> claims = new();
        private int expiryInMinutes = 5;


        public TokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public TokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public TokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public TokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            return (TokenBuilder)this.claims.Union(claims);
             
        }

        public TokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }


        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                                                   this.securityKey,
                                                   SecurityAlgorithms.HmacSha256)

                );

            return new TokenJWT(token);

        }
    }
}
