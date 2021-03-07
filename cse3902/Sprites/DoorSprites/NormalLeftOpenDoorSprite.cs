using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class NormalLeftOpenDoorSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;

        private int frameWidth;
        private int frameHeight;

        private Rectangle destination;
        private Rectangle door;

        private const float sizeIncrease = 1f;

        public NormalLeftOpenDoorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            center = startingPosition;
            this.startingPosition = startingPosition;

            door = wantedDoor;
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

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth) / 2;
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
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