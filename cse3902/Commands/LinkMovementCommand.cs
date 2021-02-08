using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class LinkMovementCommand : ICommand
    {
        private Game1 game;

        public LinkMovementCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            Vector2 direction;
            switch (key)
            {
                case Keys.Up:
                case Keys.W:
                    direction = new Vector2(0, 1);
                    break;
                case Keys.Left:
                case Keys.A:
                    direction = new Vector2(-1, 0);
                    break;
                case Keys.Down:
                case Keys.S:
                    direction = new Vector2(0, -1);
                    break;
                case Keys.Right:
                case Keys.D:
                    direction = new Vector2(1, 0);
                    break;
                default: //this should never happen
                    direction = new Vector2(0, 1);
                    break;
            }

            //game.Link.ChangeDirection(direction);
        }
    }
}