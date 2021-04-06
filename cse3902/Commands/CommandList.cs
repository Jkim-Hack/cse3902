using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902.Commands
{
    public class CommandList
    {
        private Dictionary<Keys[], ICommand> keyboardCommandMap;
        private Dictionary<Rectangle[], ICommand> leftMouseClickCommandMap;
        private Dictionary<Rectangle[], ICommand> rightMouseClickCommandMap;

        private Game1 game;

        private int windowWidth;
        private int windowHeight;
        private int hudHeight;
        private const int doorSide = DimensionConstants.MouseClickSide;

        public CommandList(Game1 game)
        {
            this.game = game;
            windowWidth = game.GraphicsDevice.Viewport.Width;
            windowHeight = game.GraphicsDevice.Viewport.Height;
            hudHeight = DimensionConstants.HudHeight;
            BuildCommands();
        }

        private void BuildCommands()
        {
            keyboardCommandMap = new Dictionary<Keys[], ICommand>()
            {
                {new Keys[] {Keys.Q}, new QuitCommand(game)},
                {new Keys[] {Keys.R}, new ResetCommand(game)},
                {new Keys[] {Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.W, Keys.A, Keys.S, Keys.D}, new LinkMovementCommand(game) },
                {new Keys[] {Keys.D1, Keys.D2}, new LinkAttackItemCommand(game) },
                {new Keys[] {Keys.D5, Keys.D6, Keys.D7, Keys.D8}, new LinkChangeWeaponCommand(game) },
                {new Keys[] {Keys.G, Keys.C, Keys.V, Keys.B}, new MoveCameraCommand(game) },
                {new Keys[] {Keys.E, Keys.P}, new PauseCommand(game)},
            };

            leftMouseClickCommandMap = new Dictionary<Rectangle[], ICommand>()
            {
                {new Rectangle[] {new Rectangle(windowWidth/2- doorSide/2, hudHeight, doorSide, doorSide),new Rectangle(windowWidth- doorSide, (windowHeight+hudHeight)/2- doorSide/2, doorSide, doorSide),new Rectangle(windowWidth/2- doorSide/2, windowHeight- doorSide, doorSide, doorSide),new Rectangle(0,(windowHeight+hudHeight)/2- doorSide/2, doorSide, doorSide) }, new ChangeRoomXYCommand(game)}
            };

            rightMouseClickCommandMap = new Dictionary<Rectangle[], ICommand>()
            {
                {new Rectangle[] {new Rectangle(windowWidth/2- doorSide/2, hudHeight, doorSide, doorSide),new Rectangle(windowWidth/2- doorSide/2, windowHeight- doorSide, doorSide, doorSide) }, new ChangeRoomZCommand(game)}
            };
        }

        public Dictionary<Keys[], ICommand> KeyboardCommands
        {
            get => keyboardCommandMap;
        }
        public Dictionary<Rectangle[], ICommand> LeftMouseClickCommands
        {
            get => leftMouseClickCommandMap;
        }
        public Dictionary<Rectangle[], ICommand> RightMouseClickCommands
        {
            get => rightMouseClickCommandMap;
        }
    }
}
