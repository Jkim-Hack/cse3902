using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

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
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, spriteTexture.Width, spriteTexture.Height);

            spriteBatch.Begin();
            //null argument used to draw entire block texture
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White);
            spriteBatch.End();

        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
    }
}
