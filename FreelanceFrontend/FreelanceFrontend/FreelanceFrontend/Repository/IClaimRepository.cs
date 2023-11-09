using System.Security.Claims;

namespace FreelanceFrontend.Repository
{
    public interface IClaimRepository
    {
        List<Claim> GetClaims(string token);
    }
}
