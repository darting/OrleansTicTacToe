using GrainInterfaces;
using GrainInterfaces.Exceptions;
using Orleans;
using System.Threading.Tasks;
using TicTacToe;

namespace Grains
{
    public class UserGrain : Grain, IUser
    {
        IGame game;
        Player player;

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task JoinGame(IGame game)
        {
            this.game = game;
            player = await game.Enter(this);
        }

        public Task Play(int x, int y)
        {
            if (game == null)
            {
                throw new NotInAnyGameException();
            }
            return game.Play(player, x, y);
        }

        public Task<IGame> CurrentGame()
        {
            return Task.FromResult(game);
        }

        public Task<Player> GetPlayer()
        {
            return Task.FromResult(player);
        }
    }
}
