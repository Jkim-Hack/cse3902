using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class CycleShownBlockCommand : ICommand
    {
        private Game1 game;

        public CycleShownBlockCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 2;
            int cycle;
            switch (id)
            {
                case 0:
                    cycle = -1;
                    break;
                case 1:
                    cycle = 1;
                    break;
                default: //this should never happen
                    cycle = 1;
                    break;
            }

            //game.cycleBlock(cycle);
        }

        public void Unexecute()
        {

        }
    }
}