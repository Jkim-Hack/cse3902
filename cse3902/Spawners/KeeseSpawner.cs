using System;
using cse3902.Entities.Enemies;
using cse3902.Interfaces;
using cse3902.Rooms;
using Microsoft.Xna.Framework;

namespace cse3902.Spawners
{
    public class KeeseSpawner : ISpawner
    {
        private Vector2 position;
        private int maxCount;
        private int currCount;
        private Rectangle destination;

        private const float spawnDelay = 1f;
        private float remainingSpawnDelay;

        private Game1 game;

        public KeeseSpawner(Game1 game, Vector2 startingPos, int count)
        {
            this.position = startingPos;
            this.maxCount = count;
            this.currCount = maxCount;
            this.game = game;
        }

        public ref Rectangle Bounds
        {
            get
            {
                int width = 1;
                int height = 1;
                Rectangle Destination = new Rectangle((int)position.X, (int)position.Y, width, height);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get => position;
        }

        public int MaxCount
        {
            get => this.maxCount;
        }

        public int CurrentCount
        {
            get => this.currCount;
        }

        public void Reset()
        {
            this.currCount = this.maxCount;
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
	     
            remainingSpawnDelay -= timer;
            if (remainingSpawnDelay <= 0 && currCount != maxCount)
            {
                remainingSpawnDelay = spawnDelay;
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            if (currCount > 0)
            {
                RoomEnemies.Instance.AddEnemy(new Keese(game, position));
                currCount--;
            }
        }
    }
}
