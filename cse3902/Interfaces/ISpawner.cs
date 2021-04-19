using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface ISpawner
    {
        public ref Rectangle Bounds { get; }
        public int Count { get; }
        public Vector2 Center { get; }
        public void Update();
        public void Draw();
        public void Reset();
    }
}
