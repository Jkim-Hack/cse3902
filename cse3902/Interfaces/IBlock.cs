using System;
using System.Collections.Generic;
using System.Text;

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
        public void Move();
        public void Draw();
    }
}
