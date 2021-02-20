using cse3902.Interfaces;

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
                    game.itemHandler.displayPrev();
                    break;
                case 1:
                    game.itemHandler.displayNext();
                    break;
                default: //this should never happen
                    game.itemHandler.displayNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}