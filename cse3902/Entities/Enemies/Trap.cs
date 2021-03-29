using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Entities.Enemies
{
    public class Trap: ITrap
    {
        private TrapSprite trapSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;


        private ICollidable collidable;

        public Trap(Game1 game, Vector2 start, Vector2 direction)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            //sprite sheet is 1 row, 2 columns
            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateTrapSprite(game.SpriteBatch, startingPos);
            this.direction = direction;
            speed = 50.0f;
            travelDistance = 50;

            this.collidable = new TrapCollidable(this, this.Damage);
        }

        public int Damage
        {
            get => 3;
        }

        public ref Rectangle Bounds
        {
            get => ref this.trapSprite.Box;
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public Vector2 Center
        {
            get => this.center;
            set => this.center = value;
        }

        public Vector2 PreviousCenter
        {
            //todo: not yet implemented correctly for trap
            get => this.center;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
        
        public void Update(GameTime gameTime)
        {
            this.trapSprite.Update(gameTime);
        }

        public void Draw()
        {
            this.trapSprite.Draw();
        }

    }
}
