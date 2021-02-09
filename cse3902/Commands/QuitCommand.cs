using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class QuitCommand : ICommand
    {
        private Game1 game;
	    
	    public QuitCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Keys key)
        {
            game.Exit();
        }

        public void Unexecute(Keys[] keyset)
        {

        }

    }
}
