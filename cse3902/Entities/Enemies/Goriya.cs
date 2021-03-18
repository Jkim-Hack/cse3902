using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;

namespace cse3902.Entities.Enemies
{
    public class Goriya : IEntity
    {
        private GoriyaSprite goriyaSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;

        public Goriya(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            goriyaSprite = (GoriyaSprite)EnemySpriteFactory.Instance.CreateGoriyaSprite(game.SpriteBatch, center);
            speed = 25.0f;
            travelDistance = 0;
            shoveDistance = -10;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 10;
        }

        public Vector2 Center
        {
            get => this.center;
        }

        public ref Rectangle Bounds
        {
            get => ref goriyaSprite.Box;
        }

        public void Attack()
        {
            
        }

        public void ChangeDirection(Vector2 direction)
        {
            //direction vector of (0,0) gives opposite direction
            if (direction == new Vector2(0, 0))
            {
                this.direction = -this.direction;
            }
            else
            {
                this.direction = direction;
            }
            ChangeSpriteDirection(direction);

        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            //this.goriyaSprite.Damaged = true;
        }

        public void Die()
        {
            
        }

        public void BeShoved()
        {
            this.shoveDistance = 20;
            this.shoveDirection = -this.direction;
        }

        public void StopShove()
        {
            this.shoveDistance = 0;
        }

        public void Update(GameTime gameTime)
        {
            this.collidable.ResetCollisions();
            if (this.shoveDistance > -10) ShoveMovement();
            else RegularMovement(gameTime);
        }

        private void ShoveMovement()
        {
            this.CenterPosition += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                travelDistance = 125;

                RandomDirection();
            }
            else travelDistance--;

            goriyaSprite.Update(gameTime);
        }

        private void RandomDirection()
        {
            Random rand = new System.Random();
            int choice = rand.Next(0, 4);

            switch (choice)
            {
                case 0:
                    ChangeDirection(new Vector2(1, 0));
                    break;
                case 1:
                    ChangeDirection(new Vector2(-1, 0));
                    break;
                case 2:
                    ChangeDirection(new Vector2(0, 1));
                    break;
                case 3:
                    ChangeDirection(new Vector2(0, -1));
                    break;
                default:
                    break;
            }
        }

        private void ChangeSpriteDirection(Vector2 direction)
        {
            if (direction == new Vector2(0, 0))
            {
                //direction vector of (0,0) indicates just reverse the current direction
                if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.RightFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.LeftFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.RightFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.UpFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.DownFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.DownFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.UpFacing;
                }

                return;
            }

            if (direction.X > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.RightFacing;
            }
            else if (direction.X < 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
            }
            else if (direction.Y > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.DownFacing;
            }
            else
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.UpFacing;
            }
        }

        public void Draw()
        {
            this.goriyaSprite.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                goriyaSprite.Center = value;
            }
        }

        public int Damage
        {
            get => 2;
        }

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value;
            }
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}