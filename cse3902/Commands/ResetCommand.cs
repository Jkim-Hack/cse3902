using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Input;

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
        }

        public void Unexecute()
        {

        }

    }
}
