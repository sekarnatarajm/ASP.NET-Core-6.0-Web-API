using Agree.API.Model;

namespace Agree.API.Services
{
    public class UserService : IUserService
    {
        protected readonly ITokenService _tokenService;
        public UserService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public string Authentication(Authenticatoin authenticatoin)
        {
            if (!string.IsNullOrEmpty(authenticatoin.Username))
               return _tokenService.GenerateToken(authenticatoin);
            return "";
        }
    }
}
