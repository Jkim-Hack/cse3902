using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Rooms
{
    class InteriorSprite: ISprite
    {

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private Rectangle[] frames;
        private int currentFrame;

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

        public InteriorSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 centerPosition, int roomNum)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;
            frames = new Rectangle[rows * columns];

            currentFrame = roomNum;

            this.center = centerPosition;

            distributeFrames(columns, rows);
        }

        private void distributeFrames(int columns, int rows)
        {
            int totalFrames = columns * rows;
            for (int i = 0; i < totalFrames; i++)
            {
                int row = (int)((float)i / (float)columns);
                int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, 0.8f);
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
