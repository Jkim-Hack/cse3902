﻿using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace cse3902.Sprites
{
    public class NormalLeftOpenDoorSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private int frameWidth;
        private int frameHeight;

        private List<Rectangle> hitboxes;
        private Rectangle door;

        private const float sizeIncrease = 1f;

        public NormalLeftOpenDoorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            center = startingPosition;

            door = wantedDoor;
            hitboxes = new List<Rectangle>();
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, door, Color.White, 0, origin, SpriteEffects.None, 0.9f);
        }

        public int Update(GameTime gameTime)
        {
            //nothing to update
            return 0;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public List<Rectangle> Boxes
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth) / 2;
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(-destination.Width, -destination.Height / 2);
                hitboxes.Add(destination);

                int openHeight = 18;
                height = (width - openHeight) / 2;

                destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(0, -frameHeight / 2);
                hitboxes.Add(destination);

                destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(0, openHeight / 2);
                hitboxes.Add(destination);

                return hitboxes;
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
    }
}