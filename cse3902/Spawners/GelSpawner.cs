﻿using cse3902.Entities.Enemies;
using cse3902.Interfaces;
using cse3902.Rooms;
using Microsoft.Xna.Framework;

namespace cse3902.Spawners
{
    public class GelSpawner : ISpawner
    {
        private Vector2 position;
        private int maxCount;
        private int easyCount;
        private int currCount;
        private Rectangle destination;

        private float spawnDelay;
        private float remainingSpawnDelay;

        private Game1 game;

        public GelSpawner(Game1 game, Vector2 startingPos, int count)
        {
            this.position = startingPos;
            easyCount = count;
            this.maxCount = count * SettingsValues.Instance.GetValue(SettingsValues.Variable.GelSpawnerCountMultiplier);
            this.currCount = maxCount;
            this.game = game;
            spawnDelay = SettingsValues.Instance.GetValue(SettingsValues.Variable.GelSpawnDelay);
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
            this.maxCount = easyCount * SettingsValues.Instance.GetValue(SettingsValues.Variable.GelSpawnerCountMultiplier);
            this.currCount = this.maxCount;
            spawnDelay = SettingsValues.Instance.GetValue(SettingsValues.Variable.GelSpawnDelay);
            remainingSpawnDelay = 0;
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
	     
            remainingSpawnDelay -= timer;
            if (remainingSpawnDelay <= 0 && currCount > 0)
            {
                remainingSpawnDelay = spawnDelay;
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            if (currCount > 0)
            {
                RoomEnemies.Instance.AddEnemy(new Gel(game, position));
                currCount--;
            }
        }
    }
}
