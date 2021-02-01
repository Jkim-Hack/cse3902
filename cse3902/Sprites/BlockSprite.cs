﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace cse3902.Sprites
{
    public class BlockSprite: ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;


        public BlockSprite(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;

        }

        public Vector2 StartingPosition
        {
            get => startingPosition;
            set
            {
                startingPosition = value;
                Center = value;
            }
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public void Draw()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
    }
}
