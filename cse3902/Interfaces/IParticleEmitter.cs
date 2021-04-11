using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Interfaces
{
    public interface IParticleEmmiter
    {
        public bool AnimationDone { get; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
