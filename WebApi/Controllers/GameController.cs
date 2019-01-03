using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IClusterClient clusterClient;
        private readonly IAuthService authService;

        public GameController(IClusterClient clusterClient, IAuthService authService)
        {
            this.clusterClient = clusterClient;
            this.authService = authService;
        }

        private IActionResult OK<T>(T data)
        {
            return Ok(new ApiResult
            {
                Code = 200,
                Data = data
            });
        }

        [HttpGet]
        public async Task<IActionResult> Authenticate(string token)
        {
            var userId = await authService.Authenticate(token);
            return OK(userId);
        }

        [HttpGet]
        public async Task<IActionResult> JoinGame(string token, string gameId)
        {
            var userId = await authService.Authenticate(token);
            var user = clusterClient.GetGrain<IUser>(userId);
            var game = clusterClient.GetGrain<IGame>(gameId);
            await user.JoinGame(game);
            return OK(await game.GetState());
        }

        [HttpGet]
        public async Task<IActionResult> PlayGame(string token, string gameId, int x, int y)
        {
            var userId = await authService.Authenticate(token);
            var user = clusterClient.GetGrain<IUser>(userId);
            await user.Play(x, y);
            var game = await user.CurrentGame();
            return OK(await game.GetState());
        }


    }
}
