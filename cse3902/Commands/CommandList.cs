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
                {new Keys[] {Keys.D3, Keys.D4, Keys.D5}, new BuyArrowsCommand(game) },
                {new Keys[] {Keys.D7, Keys.D8, Keys.D9, Keys.D0}, new LinkChangeWeaponCommand(game) },
                {new Keys[] {Keys.G, Keys.C, Keys.V, Keys.B}, new MoveCameraCommand(game) },
                {new Keys[] {Keys.E, Keys.P}, new PauseCommand(game)},
                {new Keys[] {Keys.K, Keys.L}, new ChangeHealthCommand(game)},
                {new Keys[] {Keys.Space}, new ClearEnemiesCommand(game)},
            };

            leftMouseClickCommandMap = new Dictionary<Rectangle[], ICommand>()
            {
                {new Rectangle[] {new Rectangle(windowWidth/2- doorSide/2, hudHeight, doorSide, doorSide),new Rectangle(windowWidth- doorSide, (windowHeight+hudHeight)/2- doorSide/2, doorSide, doorSide),new Rectangle(windowWidth/2- doorSide/2, windowHeight- doorSide, doorSide, doorSide),new Rectangle(0,(windowHeight+hudHeight)/2- doorSide/2, doorSide, doorSide) }, new ChangeRoomXYCommand(game)}
            };
            List<SettingsManager.Setting> sList = new List<SettingsManager.Setting>();
            Rectangle[] rList = new Rectangle[SettingsDisplay.Instance.ModeRectangles.Count];
            int i = 0;
            foreach (SettingsManager.Setting setting in SettingsDisplay.Instance.ModeRectangles.Keys)
            {
                sList.Add(setting);
                Rectangle r = SettingsDisplay.Instance.ModeRectangles[setting];
                r.X = r.X * DimensionConstants.scale - (int)DimensionConstants.SettingOffset.X;
                r.Y = r.Y * DimensionConstants.scale + DimensionConstants.HudHeight - (int)DimensionConstants.SettingOffset.Y;
                r.Width *= DimensionConstants.scale;
                r.Height *= DimensionConstants.scale;
                rList[i] = r;
                i++;
            }
            leftMouseClickCommandMap.Add(rList, new UpdateSettingCommand(sList));

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
