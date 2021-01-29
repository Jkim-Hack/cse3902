using System;
using System.Collections.Generic;
using cse3902.Commands;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cse3902
{
    public class KeyboardController : IController
    {
        private CommandList commandList;

	    public KeyboardController(Game1 game)
        { 
            this.commandList = new CommandList(game);
        }

		public void Update()
		{
            KeyboardState KeyboardState = Keyboard.GetState();
		    foreach (Keys key in KeyboardState.GetPressedKeys()) 
	        {
                if (this.commandList.KeyboardCommands.ContainsKey(key))
                {
                    this.commandList.KeyboardCommands[key].Execute();
                }
	        }
	    }
    }
}
