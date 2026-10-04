using API.Models;

namespace API.Services.Jwtservices
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
