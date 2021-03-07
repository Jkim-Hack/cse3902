using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input; // JUST FOR TESTING

namespace cse3902.Entities.Enemies
{
    public class Keese : IEntity
    {
        private KeeseSprite keeseSprite;
        private KeeseStateMachine keeseStateMachine;
        private readonly Game1 game;

        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int radius = 80;
        private float degrees;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;

        public Keese(Game1 game)
        {
            this.game = game;
            startingPos = new Vector2(500, 200);
            center = startingPos;

            keeseSprite = (KeeseSprite)EnemySpriteFactory.Instance.CreateKeeseSprite(game.spriteBatch, center);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            degrees = 0;
            speed = 0.02f;
            shoveDistance = -10;
            shoveDirection = new Vector2(-2, 0);

            this.collidable = new EnemyCollidable(this, this.Damage);
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
            this.keeseStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
        }

        public void Die()
        {
            this.keeseStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 10;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.shoveDistance <= -10) BeShoved(); // JUST FOR TESTING

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
            var radians = degrees + (Math.PI / 180);
            var unitCirclePos = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
            CenterPosition = startingPos + (unitCirclePos * radius);

            degrees += speed;

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

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}