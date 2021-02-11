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

        public void Execute(int id)
        {
            id = id % 2;
            String direction = "";
            switch (id)
            {
                case 0:
                    direction = "up";
                    break;
                case 1:
                    direction = "down";
                    break;
                default: //this should never happen
                    direction = "up";
                    break;
            }

            //game.Link.attack(direction);
        }

        public void Unexecute()
        {

        }
    }
}