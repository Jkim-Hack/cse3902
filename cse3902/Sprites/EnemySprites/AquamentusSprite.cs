using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites.EnemySprites
{
    public class AquamentusSprite: ISprite
    {

        public enum FrameIndex
        {
            LeftFacing = 0,
            RightFacing = 2,
            UpFacing = 4,
            DownFacing = 6
        };

        public AquamentusSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {


        }

        // I question the need for this vector
        public Vector2 StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Vector2 Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Texture2D Texture => throw new NotImplementedException();

        public void Draw()
        {
            throw new NotImplementedException();
            //Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
        }

        public void Erase()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
