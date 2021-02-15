using System;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Sprites

{
    public class FireballSprite: ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private Vector2 direction;
        private Vector2 center;

        private const float speed = 0.2f;

        public FireballSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            this.direction = direction;
            this.center = startingPosition;
        }

        Vector2 ISprite.StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Vector2 ISprite.Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Texture2D ISprite.Texture => throw new NotImplementedException();

        void ISprite.Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, spriteTexture.Width, spriteTexture.Height);
            Rectangle Source = new Rectangle(spriteTexture.Width, spriteTexture.Height, spriteTexture.Width, spriteTexture.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, Source, Color.White);
            spriteBatch.End();
        }

        void ISprite.Erase()
        {
            spriteTexture.Dispose();
        }

        void ISprite.Update(GameTime gameTime)
        {
            //may need to just be = instead of +=
            center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
