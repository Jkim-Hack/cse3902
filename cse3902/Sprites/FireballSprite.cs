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
        private Vector2 center;
        private Vector2 direction;

        private const float speed = 1.3f;

        private const float sizeIncrease = 1f;


        Texture2D ISprite.Texture => throw new NotImplementedException();

        public FireballSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            this.direction = direction;
            this.center = startingPosition;
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


        public int Update(GameTime gameTime)
        {
            //may need to just be = instead of +=
            center.X += (float)(speed * direction.X);
            center.Y += (float)(speed * direction.Y);
            return 0;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * Texture.Width), (int)(sizeIncrease * Texture.Height));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, 0.6f);
        }

        public Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * Texture.Width);
                int height = (int)(sizeIncrease * Texture.Height);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                return Destination;
            }
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
    }
}
