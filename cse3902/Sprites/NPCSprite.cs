using static cse3902.Interfaces.ISprite;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class NPCSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;

        private int frameWidth;
        private int frameHeight;

        public NPCSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            center = startingPosition;
            this.startingPosition = startingPosition;
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, new Rectangle(0, 0, frameWidth, frameHeight), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            //nothing to update
        }

        public void Erase()
        {
            spriteTexture.Dispose();
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
        public Vector2 StartingPosition
        {
            get => this.startingPosition;
            set
            {
                this.startingPosition = value;
                this.center = value;
            }
        }
    }
}