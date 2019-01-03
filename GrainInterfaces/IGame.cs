using Orleans;
using System.Threading.Tasks;
using TicTacToe;

namespace GrainInterfaces
{
    public interface IGame : IGrainWithStringKey
    {
        Task<Player> Enter(IUser user);
        Task<Model> GetState();
        Task<Model> Play(Player player, int x, int y);
    }
}
