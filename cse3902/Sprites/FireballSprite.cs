using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites

{
    public class FireballSprite: ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private Vector2 direction;
        private Vector2 center;

        private const float speed = 0.2f;

        Vector2 ISprite.StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Vector2 ISprite.Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Texture2D ISprite.Texture => throw new NotImplementedException();

        public FireballSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)

        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            this.direction = direction;
            this.center = startingPosition;
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


        public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {
            //may need to just be = instead of +=
            center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, spriteTexture.Width, spriteTexture.Height);
            Rectangle Source = new Rectangle(spriteTexture.Width, spriteTexture.Height, spriteTexture.Width, spriteTexture.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, Source, Color.White);
            spriteBatch.End();
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
    }
}
