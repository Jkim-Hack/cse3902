using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902
{
    public class DisplayStaticMovableSpriteCommand : ICommand
    {
        private Game1 game;
       
        public DisplayStaticMovableSpriteCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            // 2 is the StaticMovableSprite in the list
            game.currentSpriteIndex = 2;
        }
    }
}
