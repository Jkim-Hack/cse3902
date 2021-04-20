using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface ISpawner
    {
        public ref Rectangle Bounds { get; }
        public int MaxCount { get; }
        public int CurrentCount { get; }
        public Vector2 Center { get; }
        public void Update(GameTime gameTime);
        public void Reset();
        public void SpawnEnemy();
    }
}
