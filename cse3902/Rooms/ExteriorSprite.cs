using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Rooms
{
    class ExteriorSprite: ISprite
    {

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private int frameWidth;
        private int frameHeight;
        private Rectangle destination;

        private Rectangle destination;

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public ref Rectangle Box
        {
            get
            {
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public ExteriorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 centerPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            frameWidth = texture.Width;
            frameHeight = texture.Height;

            this.center = centerPosition;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, 0.8f);
        }

        public void Erase()
        {
            
        }

        int ISprite.Update(GameTime gameTime)
        {
            return 0;
        }
    }

}
