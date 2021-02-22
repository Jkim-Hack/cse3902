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
                    game.itemHandler.CyclePrev();
                    break;
                case 1:
                    game.itemHandler.CycleNext();
                    break;
                default: //this should never happen
                    game.itemHandler.CycleNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}