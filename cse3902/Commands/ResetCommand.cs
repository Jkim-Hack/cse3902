using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Entities;

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
            game.blockHandler.Reset();
            game.linkProjectiles = new List<IProjectile>();
            game.player = new Link(game);
        }

        public void Unexecute()
        {

        }

    }
}
