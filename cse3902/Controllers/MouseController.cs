using System;
using cse3902.Commands;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cse3902
{
    public class MouseController : IController
    {
        private MouseState mouseState;
		private GraphicsDevice graphicsDevice;
		private CommandList commandList;

	    public MouseController(Game1 game)
		{
			commandList = new CommandList(game);
			this.graphicsDevice = game.GraphicsDevice;
		}

		public void Update()
		{		
            mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				if (mouseState.Position.X < graphicsDevice.Viewport.Width / 2 && mouseState.Position.Y < graphicsDevice.Viewport.Height / 2)
				{ // Quad 1
					commandList.MouseClickCommands[1].Execute();
				}
				else if (mouseState.Position.X > graphicsDevice.Viewport.Width / 2 && mouseState.Position.Y < graphicsDevice.Viewport.Height / 2)
				{ // Quad 2
					commandList.MouseClickCommands[2].Execute();
				}
				else if (mouseState.Position.X < graphicsDevice.Viewport.Width / 2 && mouseState.Position.Y > graphicsDevice.Viewport.Height / 2)
				{ // Quad 3
					commandList.MouseClickCommands[3].Execute();
				}
				else if (mouseState.Position.X > graphicsDevice.Viewport.Width / 2 && mouseState.Position.Y > graphicsDevice.Viewport.Height / 2)
				{ // Quad 4
					commandList.MouseClickCommands[4].Execute();
				}
			}
			else if (mouseState.RightButton == ButtonState.Pressed)
			{
				commandList.MouseClickCommands[0].Execute();
			}
		}
    }
}
