using Orleans;
using System.Threading.Tasks;
using TicTacToe;

namespace GrainInterfaces
{
    public interface IUser : IGrainWithStringKey
    {
        Task JoinGame(IGame game);
        Task Play(int x, int y);
        Task<Player> GetPlayer();
        Task<IGame> CurrentGame();
    }
}
