using cse3902.Commands;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace cse3902
{
    public class MouseController : IController
    {
		private CommandList commandList;

	    public MouseController(Game1 game)
		{
			commandList = new CommandList(game);
		}

		public void Update()
		{		
            MouseState currentMouseState = Mouse.GetState();
			Point pos = currentMouseState.Position;

            if (currentMouseState.LeftButton == ButtonState.Pressed) LeftClick(pos);
            if (currentMouseState.RightButton == ButtonState.Pressed) RightClick(pos);
		}

        private void LeftClick(Point pos)
        {
            foreach (Rectangle[] rectangleSet in this.commandList.LeftMouseClickCommands.Keys)
            {
                for (int i = 0; i < rectangleSet.Length; i++)
                {
                    Rectangle rectangle = rectangleSet[i];
                    if (rectangle.Contains(pos))
                    {
                        this.commandList.LeftMouseClickCommands[rectangleSet].Execute(i);
                        break;
                    }
                }
            }
        }

        private void RightClick(Point pos)
        {
            foreach (Rectangle[] rectangleSet in this.commandList.RightMouseClickCommands.Keys)
            {
                for (int i = 0; i < rectangleSet.Length; i++)
                {
                    Rectangle rectangle = rectangleSet[i];
                    if (rectangle.Contains(pos))
                    {
                        this.commandList.RightMouseClickCommands[rectangleSet].Execute(i);
                        break;
                    }
                }
            }
        }
    }
}
