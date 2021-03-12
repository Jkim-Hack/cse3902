using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IDoorSprite
    {
        public void Draw();
        public void Erase();
        public int Update(GameTime gameTime);
        public Vector2 Center { get; set; }
        public ref Rectangle Box { get; }
    }
}
