using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IParticleEmmiter
    {
        public void Update(GameTime gameTime);
        public void Draw();
    }
}
