using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Stalfos: IEntity
    {
        private StalfosSprite stalfosSprite;
        private StalfosStateMachine stalfosStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;

        public Stalfos(Game1 game)
        {
            this.game = game;
            Texture2D stalfosTexture = game.Content.Load<Texture2D>("enemies/stalfos");
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //stalfos sprite sheet is 1 row, 2 columns
            stalfosSprite = new StalfosSprite(game.spriteBatch, stalfosTexture, 1, 2, center);
            stalfosStateMachine = new StalfosStateMachine(stalfosSprite);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 80;
        }

        public Rectangle Bounds
        {
            get => stalfosSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.stalfosStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.stalfosStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.stalfosSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1;

            } else if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1;
            }

            stalfosSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        //TODO: Ientity should require clases to implement this I think
        public void Draw()
        {
            stalfosSprite.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                stalfosSprite.Center = value;
            }
        }
    }
}
