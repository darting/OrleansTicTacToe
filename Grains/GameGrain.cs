using GrainInterfaces;
using GrainInterfaces.Exceptions;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe;

namespace Grains
{
    public class GameGrain : Grain, IGame
    {
        Model gameState;
        List<IUser> players;

        public override Task OnActivateAsync()
        {
            gameState = Game.init();
            players = new List<IUser>();
            return base.OnActivateAsync();
        }

        private Player GetPlayerByIndex(int index)
        {
            if (index == 0) return Player.X;
            return Player.O;
        }

        public Task<Player> Enter(IUser user)
        {
            var existing = players.FindIndex(x => x.GetPrimaryKeyString() == user.GetPrimaryKeyString());
            if (existing >= 0)
            {
                return Task.FromResult(GetPlayerByIndex(existing));
            }
            if (players.Count >= 2)
            {
                throw new CannotJoinGameException();
            }
            players.Add(user);
            if (players.Count == 1)
            {
                return Task.FromResult(Player.X);
            }
            return Task.FromResult(Player.O);
        }

        public Task<Model> GetState()
        {
            return Task.FromResult(gameState);
        }

        public Task<Model> Play(Player player, int x, int y)
        {
            if (gameState.Result != GameResult.StillPlaying)
            {
                throw new GameIsFinishedException();
            }

            if (players.Count != 2)
            {
                throw new GameNotYetStartException();
            }

            if (gameState.NextUp != player)
            {
                throw new NotYourTurnException();
            }

            var msg = Message.NewPlay(Tuple.Create(x, y));

            var newState = Game.update(msg, gameState);

            return Task.FromResult(newState);
        }
    }
}
