using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using cse3902.Sprites;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//commented things that aren't needed now but may be useful later

namespace cse3902.Commands
{
    public class CommandList
    {
        private Dictionary<Keys, ICommand> keyboardCommandMap;
        //private Dictionary<int, ICommand> mouseClickCommandMap;

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
                { Keys.Q, new QuitCommand(game)},
                { Keys.R, new ResetCommand(game)},
                { Keys.D1, new LinkUseItemCommand(game) },
		        { Keys.D2, new LinkUseItemCommand(game) },
		        { Keys.D3, new LinkUseItemCommand(game) },
		        { Keys.D4, new LinkUseItemCommand(game) },
                { Keys.D5, new LinkUseItemCommand(game) },
                { Keys.D6, new LinkUseItemCommand(game) },
                { Keys.D7, new LinkUseItemCommand(game) },
                { Keys.D8, new LinkUseItemCommand(game) },
                { Keys.D9, new LinkUseItemCommand(game) },
                { Keys.W, new LinkMovementCommand(game) },
                { Keys.A, new LinkMovementCommand(game) },
                { Keys.S, new LinkMovementCommand(game) },
                { Keys.D, new LinkMovementCommand(game) },
                { Keys.Up, new LinkMovementCommand(game) },
                { Keys.Down, new LinkMovementCommand(game) },
                { Keys.Left, new LinkMovementCommand(game) },
                { Keys.Right, new LinkMovementCommand(game) },
                { Keys.Z, new LinkSwordAttackCommand(game) },
                { Keys.N, new LinkSwordAttackCommand(game) },
                { Keys.E, new DamageLinkCommand(game) },
                { Keys.T, new CycleShownBlockCommand(game) },
                { Keys.Y, new CycleShownBlockCommand(game) },
                { Keys.U, new CycleShownItemCommand(game) },
                { Keys.I, new CycleShownItemCommand(game) },
                { Keys.O, new CycleShownEnemyNPCCommand(game) },
                { Keys.P, new CycleShownEnemyNPCCommand(game) }
            };
            
            /*
	        mouseClickCommandMap = new Dictionary<int, ICommand>()
            {
                { 0, new QuitCommand(game)},
                { 1, new DisplayStaticStillSpriteCommand(game) },
		        { 2, new DisplayAnimatedStillSpriteCommand(game) },
		        { 3, new DisplayStaticMovableSpriteCommand(game) },
		        { 4, new DisplayAnimatedMovableSpriteCommand(game) }
		    };
            */
            
        }

        public Dictionary<Keys, ICommand> KeyboardCommands
        {
            get => keyboardCommandMap;
        }

        /*
        public Dictionary<int, ICommand> MouseClickCommands
        {
            get => mouseClickCommandMap;
        }
        */

    }
}
