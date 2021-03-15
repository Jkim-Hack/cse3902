using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;

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

            keeseSprite = (KeeseSprite)EnemySpriteFactory.Instance.CreateKeeseSprite(game.spriteBatch, center);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
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
            // Keese can't change direction
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
            if (this.shoveDistance >= 0) this.CenterPosition += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance)
            {
                direction.X = 0;
                direction.Y = 1;
            }
            else if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance)
            {
                direction.X = 0;
                direction.Y = -1;
            }
            else if (direction.Y > 0 && CenterPosition.Y > startingPos.Y + travelDistance)
            {
                direction.X = -1;
                direction.Y = 0;
            }
            else if (direction.Y < 0 && CenterPosition.Y < startingPos.Y - travelDistance)
            {
                direction.X = 1;
                direction.Y = 0;
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