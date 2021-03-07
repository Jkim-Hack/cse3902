using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    interface IBlock
    {
        public enum PushDirection
        {
            Up,
            Down,
            Left,
            Right,
            Still
        }
        public void Move(Vector2 pushDirection);
        public void Move(PushDirection pushDirection);
        public void Draw();
    }
}
