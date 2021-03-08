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
        private int frameWidth;
        private int frameHeight;
        private Rectangle destination;
        private const float sizeIncrease = 1f;


        public BlockSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPos)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            center = startingPos;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;
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

            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, 0.8f);

        }
        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
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
