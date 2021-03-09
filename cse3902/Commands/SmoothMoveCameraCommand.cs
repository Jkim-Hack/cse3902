using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{
    public class SmoothMoveCameraCommand : ICommand
    {
        private Game1 game;

        public SmoothMoveCameraCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 4;
            Vector3 direction;
            switch (id)
            {
                case 0:
                    direction = new Vector3(0, -1, 0);
                    break;
                case 1:
                    direction = new Vector3(-1, 0, 0);
                    break;
                case 2:
                    direction = new Vector3(0, 1, 0);
                    break;
                case 3:
                    direction = new Vector3(1, 0, 0);
                    break;
                default: //this should never happen
                    direction = new Vector3(0, 1, 0);
                    break;
            }
            game.camera.SmoothMoveCamera(200 * new Vector2(direction.X,direction.Y), 90);
            //game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + direction);
        }

        public void Unexecute()
        {

        }
    }
}