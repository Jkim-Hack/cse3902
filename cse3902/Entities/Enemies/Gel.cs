using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input; // JUST FOR TESTING

namespace cse3902.Entities.Enemies
{
    public class Gel : IEntity
    {
        private GelSprite gelSprite;
        private GelStateMachine gelStateMachine;
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

        public Gel(Game1 game)
        {
            this.game = game;
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //gel sprite sheet is 1 row, 2 columns
            gelSprite = (GelSprite)EnemySpriteFactory.Instance.CreateGelSprite(game.spriteBatch, startingPos);
            gelStateMachine = new GelStateMachine(gelSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
            shoveDistance = -10;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 2;
        }

        public ref Rectangle Bounds
        {
            get => ref gelSprite.Box;
        }

        public void Attack()
        {
            this.gelStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.gelStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
        }

        public void Die()
        {
            this.gelStateMachine.Die();
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

            gelSprite.Update(gameTime);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            gelSprite.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                gelSprite.Center = value;
            }
        }

        public int Damage
        {
            get => 3;
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