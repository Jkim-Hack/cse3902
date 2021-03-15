using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Entities.Enemies
{
    public class Trap: IEntity
    {
        private TrapSprite trapSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;

        public Trap(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            //gel sprite sheet is 1 row, 2 columns
            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateTrapSprite(game.spriteBatch, startingPos);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
            shoveDistance = -10;

            this.collidable = new EnemyCollidable(this, this.Damage);
        }

        public int Damage
        {
            get => 3;
        }

        public ref Rectangle Bounds
        {
            get => ref this.trapSprite.Box;
        }

        public int Health
        {
            get => 0;
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public Vector2 Center
        {
            get => this.center;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        public void TakeDamage(int damage)
        {
            //traps don't take damage
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void BeShoved()
        {
            throw new NotImplementedException();
        }
    }
}
