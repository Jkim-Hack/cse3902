using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IParticle
    {
        public bool Dead { get; }

        public void Update(GameTime gameTime);
        public void Draw();
    }
}
