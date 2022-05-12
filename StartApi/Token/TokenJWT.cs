using System.IdentityModel.Tokens.Jwt;

namespace StartApi.Token
{
    public class TokenJWT
    {
        private readonly JwtSecurityToken token;

        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
