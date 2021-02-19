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
        private Vector2 centerPosition;

        public WallMaster(Game1 game)
        {
            this.game = game;
            Texture2D wallMasterTexture = game.Content.Load<Texture2D>("wall_master");
            centerPosition = new Vector2(400, 500);

            //wallmaster sprite sheet is 4 rows, 2 columns
            wallMasterSprite = new WallMasterSprite(game.spriteBatch, wallMasterTexture, 4, 2, centerPosition);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            speed = 0.0f;
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
            wallMasterSprite.Update(gameTime, onSpriteAnimationComplete);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            wallMasterSprite.Draw();
        }
    }
}
