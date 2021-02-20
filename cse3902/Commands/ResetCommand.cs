using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class ResetCommand : ICommand
    {
        private Game1 game;
	    
	    public ResetCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            game.enemyNPCHandler.Reset();
            game.itemHandler.Reset();
        }

        public void Unexecute()
        {

        }

    }
}
