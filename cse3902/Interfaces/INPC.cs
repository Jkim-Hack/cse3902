using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface INPC
    {
        public void Update(GameTime gameTime);
        public void Draw();
        public void SetMessage(string message);

        public Vector2 Center { get; set; }
    }
}
