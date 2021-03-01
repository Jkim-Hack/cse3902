﻿using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;

namespace cse3902.Entities.Enemies
{
    public class WallMaster : IEntity
    {
        private WallMasterSprite wallMasterSprite;
        private WallMasterStateMachine wallMasterStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 center;
        private float range;
        private float traveled;

        public WallMaster(Game1 game)
        {
            this.game = game;
            center = new Vector2(500, 200);

            //wallmaster sprite sheet is 4 rows, 2 columns
            wallMasterSprite = (WallMasterSprite)EnemySpriteFactory.Instance.CreateWallMasterSprite(game.spriteBatch, center);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            direction = new Vector2(-1, 1);
            speed = 6.5f;
            range = 15;
            traveled = range;
        }

        public Rectangle Bounds
        {
            get => wallMasterSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.wallMasterStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.wallMasterStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
        }

        public void Die()
        {
            this.wallMasterStateMachine.Die();
        }

        public void Update(GameTime gameTime)
        {
            var change = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            traveled -= change;
            if (traveled <= 0)
            {
                if (direction.X > 0 && direction.Y > 0)
                {
                    direction.Y = -1;
                }
                else if (direction.X > 0 && direction.Y < 0)
                {
                    direction.X = -1;
                }
                else if (direction.X < 0 && direction.Y > 0)
                {
                    direction.X = 1;
                }
                else if (direction.X < 0 && direction.Y < 0)
                {
                    direction.Y = 1;
                }

                traveled = range;
            }

            this.CenterPosition += direction * speed * change;

            ChangeDirection(direction);
            wallMasterSprite.Update(gameTime);
        }

        public void Draw()
        {
            wallMasterSprite.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                wallMasterSprite.Center = value;
            }
        }
    }
}