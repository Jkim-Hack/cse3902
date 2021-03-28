using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Entities;
using cse3902.HUD;

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
            game.RoomHandler.Reset();
            //game.Camera.Reset();
            //game.player.Reset();
            //game.player = new Link(game);
            InventoryManager.Instance.Reset();
        }

        public void Unexecute()
        {

        }

    }
}
