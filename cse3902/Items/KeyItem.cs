using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class KeyItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private Vector2 center;

        private int currentX;
        private int currentY;

        public KeyItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;
        }

        public void Draw()
        {
            //new Rectangle((int)center.X, (int)center.Y, spriteTexture.Width, spriteTexture.Height);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, new Rectangle(currentX, currentY, 2*spriteTexture.Width, 2*spriteTexture.Height), null, Color.White);
            spriteBatch.End();
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {

        }

        public Vector2 StartingPosition
        {
            get => startingPosition;

            set
            {
                startingPosition = value;
                center = value;
                currentX = (int)value.X;
                currentY = (int)value.Y;
            }
        }

        public Vector2 Center
        {
            get => center;

            set
            {
                center = value;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }
    }
}
