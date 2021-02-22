using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Goriya: IEntity
    {
        private GoriyaSprite goriyaSprite;
        private GoriyaStateMachine goriyaStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;

        public Goriya(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("enemies/goriya_blue");
            startingPos = new Vector2(500, 200);
            center = startingPos;

            goriyaSprite = new GoriyaSprite(game.spriteBatch, linkTexture, 4, 2, center);
            goriyaStateMachine = new GoriyaStateMachine(goriyaSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
        }

        public Rectangle Bounds
        {
            get => goriyaSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.goriyaStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.goriyaStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.goriyaSprite.Erase();
        }

        public void Update(GameTime gameTime)
        {

            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = 0;
                direction.Y = 1;

            } else if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 0;
                direction.Y = -1;

            } else if (direction.Y > 0 && CenterPosition.Y > startingPos.Y + travelDistance) {

                direction.X = -1;
                direction.Y = 0;

            } else if (direction.Y < 0 && CenterPosition.Y < startingPos.Y - travelDistance) {

                direction.X = 1;
                direction.Y = 0;
            }
            
            goriyaSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            this.goriyaSprite.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                goriyaSprite.Center = value;
            }
        }
    }
}
