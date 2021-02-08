using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class LinkUseItemCommand : ICommand
    {
        private Game1 game;

        public LinkUseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            int item;
            switch (key)
            {
                case Keys.D1:
                    item = 1;
                    break;
                case Keys.D2:
                    item = 2;
                    break;
                case Keys.D3:
                    item = 3;
                    break;
                case Keys.D4:
                    item = 4;
                    break;
                case Keys.D5:
                    item = 5;
                    break;
                case Keys.D6:
                    item = 6;
                    break;
                case Keys.D7:
                    item = 7;
                    break;
                case Keys.D8:
                    item = 8;
                    break;
                case Keys.D9:
                    item = 9;
                    break;
                default: //this should never happen
                    item = 1;
                    break;
            }

            //game.Link.useItem(item);
        }
    }
}