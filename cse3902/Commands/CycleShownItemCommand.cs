using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class CycleShownItemCommand : ICommand
    {
        private Game1 game;

        public CycleShownItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            switch (key)
            {
                case Keys.U:
                    //game.ItemList.DisplayPrev();
                    break;
                case Keys.I:
                    //game.ItemList.DisplayNext();
                    break;
                default: //this should never happen
                    //game.ItemList.DisplayNext();
                    break;
            }
        }

        public void Unexecute(Keys[] keyset)
        {

        }
    }
}