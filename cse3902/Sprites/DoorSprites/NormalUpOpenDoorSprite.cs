using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class NormalUpOpenDoorSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private Rectangle dest;
        private Rectangle door;

        private const float sizeIncrease = 1f;

        public NormalUpOpenDoorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            center = startingPosition;

            door = wantedDoor;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(door.Width / 2f, door.Height / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(door.Width * sizeIncrease), (int)(door.Height * sizeIncrease));
            spriteBatch.Draw(spriteTexture, Destination, door, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.DoorTopLayer);
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
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, (int) (door.Width* sizeIncrease), (int)(door.Height *sizeIncrease));
                destination.Offset(-destination.Width / 2, -destination.Height / 2);
                dest = destination;

                return ref dest;
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
    }
}