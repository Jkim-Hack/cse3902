using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{   
    public class DisplayAnimatedStillSpriteCommand : ICommand
    {
        private Game1 game;

	    public DisplayAnimatedStillSpriteCommand(Game1 game)
        {
            this.game = game;
	    }

        public void Execute()
        {
            // 1 is the AnimatedStillSprite in the list
            //TODO: member of game class no longer exists
            //game.currentSpriteIndex = 1;
        }
    }
}
