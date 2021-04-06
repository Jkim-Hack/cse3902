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
            RoomEnemies.Instance.KillAll();
        }

        public void Unexecute()
        {
        }
    }
}