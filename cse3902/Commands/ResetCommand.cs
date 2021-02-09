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

        public void Execute(Keys key)
        {
        }

        public void Unexecute(Keys[] keyset)
        {

        }

    }
}
