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
        public void Move(Vector2 pushDirection);
        public void Move(PushDirection pushDirection);
        public void Draw();
    }
}
