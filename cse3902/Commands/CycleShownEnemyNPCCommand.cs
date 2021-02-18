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

        public void Execute(int id)
        {
            id = id % 2;
            switch (id)
            {
                case 0:
                    game.enemyNPCHandler.DisplayPrev();
                    break;
                case 1:
                    game.enemyNPCHandler.DisplayNext();
                    break;
                default: //this should never happen
                    game.enemyNPCHandler.DisplayNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}