﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902
{
    public class DisplayStaticStillSpriteCommand : ICommand
    {
        private Game1 game;
       
        public DisplayStaticStillSpriteCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            // 0 is the StaticStillSprite in the list
            game.currentSpriteIndex = 0;
            // The center of the screen
            game.spriteList[game.currentSpriteIndex].StartingPosition = new Vector2(400, 200);
        }
    }
}
