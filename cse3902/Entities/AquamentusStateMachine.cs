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

        private bool isAttacking;
        private int fireballCounter;
        private int fireballComplete;

        private Vector2 center;

        public AquamentusStateMachine(AquamentusSprite aquamentusSprite, Texture2D fireballTexture, SpriteBatch spriteBatch, Vector2 center)
        {
            this.aquamentusSprite = aquamentusSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = true;
            fireballCounter = 0;
            fireballComplete = 120;

            this.center = center;

            this.fireballTexture = fireballTexture;

            LoadFireballs();

        }

        private void LoadFireballs()
        {
            //the direction the fireballs will travel
            Vector2 direction1;
            Vector2 direction2;
            Vector2 direction3;

            //all fireballs originate in the mouth
            Vector2 location;
            location.X = this.center.X;
            location.Y = this.center.Y;

            if (aquamentusSprite.StartingFrameIndex == (int)AquamentusSprite.FrameIndex.RightFacing) {
                direction1 = new Vector2(1, 0);
                direction2 = new Vector2(3, 1);
                direction2.Normalize();
                direction3 = new Vector2(3, -1);
                direction3.Normalize();
            } else
            {
                direction1 = new Vector2(-1, 0);
                direction2 = new Vector2(-3, 1);
                direction2.Normalize();
                direction3 = new Vector2(-3, -1);
                direction3.Normalize();

                location.X += -30; //originate fireballs at mouth if facing left
            }

            fireball1 = new FireballSprite(spriteBatch, fireballTexture, location, direction1);
            fireball2 = new FireballSprite(spriteBatch, fireballTexture, location, direction2);
            fireball3 = new FireballSprite(spriteBatch, fireballTexture, location, direction3);
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

        public void TakeDamage()
        {

        }

        public void Attack()
        {
            this.IsAttacking = true;
        }

        public void Draw()
        {
            if (this.IsAttacking)
            {
                fireball1.Draw();
                fireball2.Draw();
                fireball3.Draw();
            }

            aquamentusSprite.Draw();
        }

        public void Die()
        {
            this.aquamentusSprite.Erase();
        }

        public void Update(GameTime gameTime, Vector2 center)
        {
            this.center = center;
            if (this.IsAttacking)
            {
                if (fireballCounter > fireballComplete)
                {
                    LoadFireballs();
                    fireballCounter = 0;
                } else
                {
                    fireball1.Update(gameTime);
                    fireball2.Update(gameTime);
                    fireball3.Update(gameTime);
                    fireballCounter++;
                }
                
            }
            aquamentusSprite.Update(gameTime);
        }

        //private void onSpriteAnimationComplete()
        //{
        //    if (this.IsAttacking)
        //    {
        //        this.IsAttacking = false;
        //    }
        //}

        public bool IsAttacking
        {
            get => isAttacking;
            set
            {
                isAttacking = value;
            }
        }
    }
}
