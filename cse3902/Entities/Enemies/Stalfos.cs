using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input; // JUST FOR TESTING

namespace cse3902.Entities.Enemies
{
    public class Stalfos : IEntity
    {
        private StalfosSprite stalfosSprite;
        private StalfosStateMachine stalfosStateMachine;
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

        public Stalfos(Game1 game)
        {
            this.game = game;
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //stalfos sprite sheet is 1 row, 2 columns
            stalfosSprite = (StalfosSprite)EnemySpriteFactory.Instance.CreateStalfosSprite(game.spriteBatch, center);
            stalfosStateMachine = new StalfosStateMachine(stalfosSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 80;
            shoveDistance = -10;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 10;
        }

        public ref Rectangle Bounds
        {
            get => ref stalfosSprite.Box;
        }

        public void Attack()
        {
            this.stalfosStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.stalfosStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
        }

        public void Die()
        {
            this.stalfosStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 10;
            this.shoveDirection = new Vector2(direction.X * -2, direction.Y * -2);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.shoveDistance <= -10) BeShoved(); // JUST FOR TESTING

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

            if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance)
            {
                direction.X = 1;
            }
            else if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance)
            {
                direction.X = -1;
            }

            stalfosSprite.Update(gameTime);
        }

        public void Draw()
        {
            stalfosSprite.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                stalfosSprite.Center = value;
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

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}