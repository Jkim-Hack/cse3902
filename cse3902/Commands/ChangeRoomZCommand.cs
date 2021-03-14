using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{
    public class ChangeRoomZCommand : ICommand
    {
        private Game1 game;

        public ChangeRoomZCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 2;
            Vector3 direction;
            switch (id)
            {
                case 0:
                    direction = new Vector3(0, 0, 1);
                    break;
                case 1:
                    direction = new Vector3(0, 0, -1);
                    break;
                default: //this should never happen
                    direction = new Vector3(0, 0, 1);
                    break;
            }

            game.roomHandler.LoadNewRoom(direction);
        }

        public void Unexecute()
        {

        }
    }
}