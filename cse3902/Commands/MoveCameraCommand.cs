using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902.Commands
{
    public class MoveCameraCommand : ICommand
    {
        private Game1 game;

        public MoveCameraCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % CommandConstants.MoveCameraCommandCount;
            Vector2 direction;
            switch (id)
            {
                case 0:
                    direction = new Vector2(0, -1);
                    break;
                case 1:
                    direction = new Vector2(-1, 0);
                    break;
                case 2:
                    direction = new Vector2(0, 1);
                    break;
                case 3:
                    direction = new Vector2(1, 0);
                    break;
                default: //this should never happen
                    direction = new Vector2(0, 1);
                    break;
            }

            game.Camera.MoveCamera(CommandConstants.MoveCameraDistance * direction);
        }

        public void Unexecute()
        {

        }
    }
}