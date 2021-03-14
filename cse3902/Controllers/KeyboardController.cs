using cse3902.Commands;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace cse3902
{
    public class KeyboardController : IController
    {
        private CommandList commandList;
        private KeyboardState previousKeyboardState;

	    public KeyboardController(Game1 game)
        { 
            this.commandList = new CommandList(game);
            previousKeyboardState = new KeyboardState();
        }

		public void Update()
		{
            KeyboardState currentKeyboardState = Keyboard.GetState();

            foreach (Keys[] keySet in this.commandList.KeyboardCommands.Keys)
            {
                bool aCurrentKeyPressed = false;
                bool aPreviousKeyPressed = false;

                for (int i=0; i<keySet.Length; i++)
                {
                    Keys key = keySet[i];
                    if (currentKeyboardState.IsKeyDown(key)) //key is down, so don't need to "undo"
                    {
                        this.commandList.KeyboardCommands[keySet].Execute(i);
                        aCurrentKeyPressed = true;
                        break;
                    }
                    else if (previousKeyboardState.IsKeyDown(key)) //key wasn't down, so need to check if it was already down
                    {
                        aPreviousKeyPressed = true;
                    }
                }

                if (aPreviousKeyPressed && !aCurrentKeyPressed) this.commandList.KeyboardCommands[keySet].Unexecute();
            }

            previousKeyboardState = currentKeyboardState;
        }
    }
}
