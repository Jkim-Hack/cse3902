using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class WallMaster: IEntity
    {
        private WallMasterSprite wallMasterSprite;
        private WallMasterStateMachine wallMasterStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;

        public WallMaster(Game1 game)
        {
            this.game = game;
            Texture2D wallMasterTexture = game.Content.Load<Texture2D>("enemies/wall_master");
            startingPos = new Vector2(500, 200);
            center = startingPos;

            //wallmaster sprite sheet is 4 rows, 2 columns
            wallMasterSprite = new WallMasterSprite(game.spriteBatch, wallMasterTexture, 4, 2, center);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            direction = new Vector2(-1, 1);
            speed = 50.0f;
            travelDistance = 80;
        }

        public Rectangle Bounds
        {
            get => wallMasterSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.wallMasterStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.wallMasterStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.wallMasterSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X > 0 && direction.Y > 0 && CenterPosition.Y > startingPos.Y + travelDistance) {

                direction.Y = -1;

            } else if (direction.X > 0 && direction.Y < 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1;

            } else if (direction.X < 0 && direction.Y > 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1;

            } else if (direction.X < 0 && direction.Y < 0 && CenterPosition.Y < startingPos.Y - travelDistance) {

                direction.Y = 1;
            }

            ChangeDirection(direction);
            wallMasterSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            wallMasterSprite.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                wallMasterSprite.Center = value;
            }
        }
    }
}
