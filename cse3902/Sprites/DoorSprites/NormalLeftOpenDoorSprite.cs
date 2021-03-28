using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class NormalLeftOpenDoorSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Texture2D blackTexture;
        private Vector2 center;

        private Rectangle dest;
        private Rectangle door;

        private bool bombedDoor;

        private const float sizeIncrease = 1f;

        public NormalLeftOpenDoorSprite(SpriteBatch spriteBatch, Texture2D blackTexture, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor, bool bombed)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            center = startingPosition;

            door = wantedDoor;
            bombedDoor = bombed;
            this.blackTexture = blackTexture;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(door.Width / 2f, door.Height / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(door.Width * sizeIncrease), (int)(door.Height * sizeIncrease));
            if (bombedDoor) spriteBatch.Draw(blackTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.BombedDoorBackgroundLayer);
            spriteBatch.Draw(spriteTexture, Destination, door, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.TopBackgroundLayer);
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
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, (int)(door.Width * sizeIncrease)-2, (int)(door.Height * sizeIncrease)/2);
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