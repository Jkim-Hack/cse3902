using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class LinkSwordAttackCommand : ICommand
    {
        private Game1 game;

        public LinkSwordAttackCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            String direction = "";
            switch (key)
            {
                case Keys.Z:
                    direction = "up";
                    break;
                case Keys.N:
                    direction = "down";
                    break;
                default: //this should never happen
                    direction = "up";
                    break;
            }

            //game.Link.attack(direction);
        }
    }
}