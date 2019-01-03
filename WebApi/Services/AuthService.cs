using System.Threading.Tasks;

namespace WebApi.Services
{
    public class AuthService : IAuthService
    {
        public Task<string> Authenticate(string token)
        {
            return Task.FromResult(token);
        }
    }
}
