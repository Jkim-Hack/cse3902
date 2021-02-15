using System;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Sprites

{
    public class FireballSprite: ISprite
    {
        public FireballSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)
        {

        }

        Vector2 ISprite.StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Vector2 ISprite.Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Texture2D ISprite.Texture => throw new NotImplementedException();

        void ISprite.Draw()
        {
            throw new NotImplementedException();
        }

        void ISprite.Erase()
        {
            throw new NotImplementedException();
        }

        void ISprite.Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
