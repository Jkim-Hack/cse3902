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

        public void Execute(int id)
        {
            id = id % 4;
            Vector2 direction;
            switch (id)
            {
                case 0:
                    direction = new Vector2(0, 1);
                    break;
                case 1:
                    direction = new Vector2(-1, 0);
                    break;
                case 2:
                    direction = new Vector2(0, -1);
                    break;
                case 3:
                    direction = new Vector2(1, 0);
                    break;
                default: //this should never happen
                    direction = new Vector2(0, 1);
                    break;
            }

            //game.Link.ChangeDirection(direction);
        }

        public void Unexecute()
        {
            //game.Link.ChangeDirection(new Vector2(0, 0));
        }
    }
}