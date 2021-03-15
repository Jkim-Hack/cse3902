using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

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
        
        public CommandList(Game1 game)
        {
            this.game = game;
            windowWidth = game.GraphicsDevice.Viewport.Width;
            windowHeight = game.GraphicsDevice.Viewport.Height;
            BuildCommands();
        }

        private void BuildCommands()
        {
            keyboardCommandMap = new Dictionary<Keys[], ICommand>()
            {
                {new Keys[] {Keys.Q}, new QuitCommand(game)},
                {new Keys[] {Keys.R}, new ResetCommand(game)},
                {new Keys[] {Keys.E}, new DamageLinkCommand(game)},
                {new Keys[] {Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.W, Keys.A, Keys.S, Keys.D}, new LinkMovementCommand(game) },
                {new Keys[] {Keys.D1, Keys.D2, Keys.D3, Keys.D4}, new LinkUseItemCommand(game) },
                {new Keys[] {Keys.D5, Keys.D6, Keys.D7, Keys.D8}, new LinkChangeWeaponCommand(game) },
                {new Keys[] {Keys.Z, Keys.N}, new LinkSwordAttackCommand(game) },
                {new Keys[] {Keys.G, Keys.C, Keys.V, Keys.B}, new MoveCameraCommand(game) },
                {new Keys[] {Keys.L, Keys.M, Keys.OemComma, Keys.OemPeriod}, new SmoothMoveCameraCommand(game) }
            };

            leftMouseClickCommandMap = new Dictionary<Rectangle[], ICommand>()
            {
                {new Rectangle[] {new Rectangle(windowWidth/2-50,0,100,100),new Rectangle(windowWidth-100,windowHeight/2-50,100,100),new Rectangle(windowWidth/2-50,windowHeight-100,100,100),new Rectangle(0,windowHeight/2-50,100,100)}, new ChangeRoomXYCommand(game)}
            };

            rightMouseClickCommandMap = new Dictionary<Rectangle[], ICommand>()
            {
                {new Rectangle[] {new Rectangle(windowWidth/2-50,0,100,100),new Rectangle(windowWidth/2-50,windowHeight-100,100,100)}, new ChangeRoomZCommand(game)}
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
