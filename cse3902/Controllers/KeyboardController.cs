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
        private KeyboardState PreviousKeyboardState;

	    public KeyboardController(Game1 game)
        { 
            this.commandList = new CommandList(game);
            PreviousKeyboardState = new KeyboardState();
        }

		public void Update()
		{
            KeyboardState CurrentKeyboardState = Keyboard.GetState();

            foreach (Keys[] keySet in this.commandList.KeyboardCommands.Keys)
            {
                bool aCurrentKeyPressed = false;
                bool aPreviousKeyPressed = false;

                for (int i=0; i<keySet.Length; i++)
                {
                    Keys key = keySet[i];
                    if (CurrentKeyboardState.IsKeyDown(key)) //key is down, so don't need to "undo"
                    {
                        this.commandList.KeyboardCommands[keySet].Execute(i);
                        aCurrentKeyPressed = true;
                        break;
                    }
                    else if (PreviousKeyboardState.IsKeyDown(key)) //key wasn't down, so need to check if it was already down
                    {
                        aPreviousKeyPressed = true;
                    }
                }

                if (aPreviousKeyPressed && !aCurrentKeyPressed) this.commandList.KeyboardCommands[keySet].Unexecute();
            }

            PreviousKeyboardState = CurrentKeyboardState;
        }
    }
}
