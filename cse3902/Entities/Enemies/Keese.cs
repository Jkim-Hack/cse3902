using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;

namespace cse3902.Entities.Enemies
{
    public class Keese : IEntity
    {
        private KeeseSprite keeseSprite;
        private KeeseStateMachine keeseStateMachine;
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

        public Keese(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            keeseSprite = (KeeseSprite)EnemySpriteFactory.Instance.CreateKeeseSprite(game.SpriteBatch, center);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            speed = 30.0f;
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
            get => ref keeseSprite.Box;
        }

        public void Attack()
        {
            this.keeseStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            //direction vector of (0,0) indicates just reverse the current direction
            if (direction == new Vector2(0, 0))
            {
                this.direction.X = -this.direction.X;
                this.direction.Y = -this.direction.Y;
            }
            else
            {
                this.direction.X = direction.X;
                this.direction.Y = direction.Y;
            }
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
        }

        public void Die()
        {
            this.keeseStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 10;
            this.shoveDirection = new Vector2(direction.X * -2, direction.Y * -2);
        }

        public void Update(GameTime gameTime)
        {

            if (this.shoveDistance > -10) ShoveMovement();
            else RegularMovement(gameTime);
        }

        private void ShoveMovement()
        {
            if (this.shoveDistance >= 0)
            {
                this.CenterPosition += shoveDirection;
                this.startingPos += shoveDirection;
            }
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                Random rand = new System.Random();
                int choice = rand.Next(0, 8);
                travelDistance = 100;

                switch (choice)
                {
                    case 0:
                        direction.X = 1;
                        direction.Y = 0;
                        break;
                    case 1:
                        direction.X = -1;
                        direction.Y = 0;
                        break;
                    case 2:
                        direction.X = 0;
                        direction.Y = 1;
                        break;
                    case 3:
                        direction.X = 0;
                        direction.Y = -1;
                        break;
                    case 4:
                        direction.X = 0.7f;
                        direction.Y = 0.7f;
                        break;
                    case 5:
                        direction.X = 0.7f;
                        direction.Y = -0.7f;
                        break;
                    case 6:
                        direction.X = -0.7f;
                        direction.Y = 1;
                        break;
                    case 7:
                        direction.X = -0.7f;
                        direction.Y = -0.7f;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                travelDistance--;
            }

            keeseSprite.Update(gameTime);
        }

        public void Draw()
        {
            this.keeseSprite.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                keeseSprite.Center = value;
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