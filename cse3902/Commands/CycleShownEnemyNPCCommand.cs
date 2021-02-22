﻿using cse3902.Interfaces;

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
                    game.enemyNPCHandler.cyclePrev();
                    break;
                case 1:
                    game.enemyNPCHandler.cycleNext();
                    break;
                default: //this should never happen
                    game.enemyNPCHandler.cycleNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}