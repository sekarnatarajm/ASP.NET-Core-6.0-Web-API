using Agree.API.Model;

namespace Agree.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(Authenticatoin authenticatoin);
    }
}
