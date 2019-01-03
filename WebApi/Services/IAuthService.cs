using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string token);
    }
}
