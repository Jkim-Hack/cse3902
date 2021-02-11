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

        public void Execute(int id)
        {
            id = id % 2;
            switch (id)
            {
                case 0:
                    //game.ItemList.DisplayPrev();
                    break;
                case 1:
                    //game.ItemList.DisplayNext();
                    break;
                default: //this should never happen
                    //game.ItemList.DisplayNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}