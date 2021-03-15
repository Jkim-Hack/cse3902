using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IBlock : ICollidableItemEntity
    {
        public enum PushDirection
        {
            Up,
            Down,
            Left,
            Right,
            Still
        }
        public ref Rectangle Bounds { get; }
        public Vector2 Center { get; }
        public void Interact(Vector2 pushDirection);
        public void Interact(PushDirection pushDirection);
        public void Draw();
    }
}
