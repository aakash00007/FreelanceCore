using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreelanceFrontend.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        public List<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            return securityToken.Claims.ToList();
        }
    }
}
