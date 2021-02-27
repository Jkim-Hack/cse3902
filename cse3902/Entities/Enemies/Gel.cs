using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;
using cse3902.SpriteFactory;

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
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //gel sprite sheet is 1 row, 2 columns
            gelSprite = (GelSprite) EnemySpriteFactory.Instance.CreateGelSprite(game.spriteBatch, startingPos);
            gelStateMachine = new GelStateMachine(gelSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
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
            this.gelStateMachine.TakeDamage();
        }

        public void Die()
        {
            this.gelStateMachine.Die();
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

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                gelSprite.Center = value;
            }
        }
    }
}
