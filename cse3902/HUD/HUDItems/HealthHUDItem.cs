using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.HUD.HUDItems
{
    public class HealthHUDItem : IHUDItem
    {
        public HealthHUDItem()
        {
        }

        public Vector2 Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Texture2D Texture => throw new NotImplementedException();

        public ref Rectangle Box => throw new NotImplementedException();

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void Erase()
        {
            throw new NotImplementedException();
        }

        public int Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
