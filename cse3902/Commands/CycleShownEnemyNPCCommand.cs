using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class CycleShownEnemyNPCCommand : ICommand
    {
        private Game1 game;

        public CycleShownEnemyNPCCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            int cycle;
            switch (key)
            {
                case Keys.O:
                    cycle = -1;
                    break;
                case Keys.P:
                    cycle = 1;
                    break;
                default: //this should never happen
                    cycle = 1;
                    break;
            }

            //game.cycleEnemyNPC(cycle);
        }
    }
}