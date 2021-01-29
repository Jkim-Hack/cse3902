using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using cse3902.Sprites;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Commands
{
    public class CommandList
    {
        private Dictionary<Keys, ICommand> keyboardCommandMap;
        private Dictionary<int, ICommand> mouseClickCommandMap;

        private Game1 game;
        
        public CommandList(Game1 game)
        {
            this.game = game;
            BuildCommands();
        }

        private void BuildCommands()
        {
            keyboardCommandMap = new Dictionary<Keys, ICommand>()
            {
                { Keys.D0, new QuitCommand(game)},
                { Keys.D1, new DisplayStaticStillSpriteCommand(game) },
		        { Keys.D2, new DisplayAnimatedStillSpriteCommand(game) },
		        { Keys.D3, new DisplayStaticMovableSpriteCommand(game) },
		        { Keys.D4, new DisplayAnimatedMovableSpriteCommand(game) }
		    };
            
	        mouseClickCommandMap = new Dictionary<int, ICommand>()
            {
                { 0, new QuitCommand(game)},
                { 1, new DisplayStaticStillSpriteCommand(game) },
		        { 2, new DisplayAnimatedStillSpriteCommand(game) },
		        { 3, new DisplayStaticMovableSpriteCommand(game) },
		        { 4, new DisplayAnimatedMovableSpriteCommand(game) }
		    };
        }

        public Dictionary<Keys, ICommand> KeyboardCommands
        {
            get => keyboardCommandMap;
        }

        public Dictionary<int, ICommand> MouseClickCommands
        {
            get => mouseClickCommandMap;
        }

    }
}
