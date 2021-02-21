using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites.EnemySprites;
using cse3902.Sprites;

namespace cse3902.Entities {

    public class AquamentusStateMachine : IEntityStateMachine
    {
        private AquamentusSprite aquamentusSprite;
        private SpriteBatch spriteBatch;

        private Texture2D fireballTexture;

        private ISprite fireball1;
        private ISprite fireball2;
        private ISprite fireball3;

        public AquamentusStateMachine(AquamentusSprite aquamentusSprite, Texture2D fireballTexture, SpriteBatch spriteBatch)
        {
            this.aquamentusSprite = aquamentusSprite;
            this.spriteBatch = spriteBatch;

            this.fireballTexture = fireballTexture;

            LoadFireballs();
        }

        private void LoadFireballs()
        {

            //TODO: tweak this to position the fireballs at the mouth of aquamentus



            Vector2 startingPosition = new Vector2(0.0f, 0.0f);

            //get a normalized direction vectors for the fireballs

            Vector2 direction1 = Vector2.Normalize(new Vector2(-1.0f, .5f));
            Vector2 direction2 = Vector2.Normalize(new Vector2(-1.0f, .0f));
            Vector2 direction3 = Vector2.Normalize(new Vector2(-1.0f, -.5f));

            if (aquamentusSprite.StartingFrameIndex == (int)AquamentusSprite.FrameIndex.RightFacing)
            {
                direction1 = Vector2.Normalize(new Vector2(1.0f, .5f));
                direction2 = Vector2.Normalize(new Vector2(1.0f, .0f));
                direction3 = Vector2.Normalize(new Vector2(1.0f, -.5f));

                //starting position will also need to be changed
            }

            fireball1 = new FireballSprite(spriteBatch, fireballTexture, startingPosition, direction1);
            fireball2 = new FireballSprite(spriteBatch, fireballTexture, startingPosition, direction2);
            fireball3 = new FireballSprite(spriteBatch, fireballTexture, startingPosition, direction3);
        }

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection.X > 0)
            {
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.RightFacing;
            } else
            {
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.LeftFacing;
            }


        }

        //TODO: takedamage and attack should probably be IEntity methods

        public void TakeDamage()
        {

        }

        public void Attack()
        {
            aquamentusSprite.IsAttacking = true;
        }

        public void Draw()
        {
            if (aquamentusSprite.IsAttacking)
            {

            }
            aquamentusSprite.Draw();
        }

        public void Update(GameTime gameTime)
        {
            
            aquamentusSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        private void onSpriteAnimationComplete()
        {
            if (aquamentusSprite.IsAttacking)
            {
                aquamentusSprite.IsAttacking = false;
            }
        }
    }
}
