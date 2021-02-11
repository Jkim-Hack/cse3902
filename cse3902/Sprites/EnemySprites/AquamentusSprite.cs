using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites.EnemySprites
{
    public class AquamentusSprite: ISprite
    {
        public AquamentusSprite()
        {
        }

        public Vector2 StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Texture2D Texture => throw new NotImplementedException();

        public void Draw()
        {
            throw new NotImplementedException();
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
