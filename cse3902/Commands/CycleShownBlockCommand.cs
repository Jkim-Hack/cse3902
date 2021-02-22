using cse3902.Interfaces;

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
            switch (id)
            {
                case 0:
                    game.blockHandler.cyclePrev();
                    break;
                case 1:
                    game.blockHandler.cycleNext();
                    break;
                default: //this should never happen
                    game.blockHandler.cycleNext();
                    break;
            }
        }

        public void Unexecute()
        {

        }
    }
}