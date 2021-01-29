using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{   
    public class DisplayAnimatedMovableSpriteCommand : ICommand
    {
        private Game1 game;

	    public DisplayAnimatedMovableSpriteCommand(Game1 game)
        {
            this.game = game;
	    }

        public void Execute()
        {
            // 3 is the AnimatedMovableSprite in the list
            game.currentSpriteIndex = 3;
            // Start on the very left center
            game.spriteList[game.currentSpriteIndex].StartingPosition = new Vector2(0, 200);
	    }
    }
}
