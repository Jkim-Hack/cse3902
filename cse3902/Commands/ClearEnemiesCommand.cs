using cse3902.Interfaces;
using cse3902.Entities;
using cse3902.Rooms;

namespace cse3902.Commands
{
    public class ClearEnemiesCommand : ICommand
    {
        private Game1 game;

        public ClearEnemiesCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning() && SettingsValues.Instance.GetValue(SettingsValues.Variable.SpaceKillEnemies)!=0) RoomEnemies.Instance.KillAll();
        }

        public void Unexecute()
        {
        }
    }
}