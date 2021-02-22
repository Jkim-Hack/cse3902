using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

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

        public Gel(Game1 game)
        {
            this.game = game;
            Texture2D gelTexture = game.Content.Load<Texture2D>("enemies/gel");
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //gel sprite sheet is 1 row, 2 columns
            gelSprite = new GelSprite(game.spriteBatch, gelTexture, 1, 2, center);
            gelStateMachine = new GelStateMachine(gelSprite);
            direction = new Vector2(-1, 0);
            speed = 25.0f;
            travelDistance = 50;
        }

        public Rectangle Bounds
        {
            get => gelSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.gelStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.gelStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.gelSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {

            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1;

            } else if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1;
            }

            gelSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            gelSprite.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                gelSprite.Center = value;
            }
        }
    }
}
