using cse3902.Interfaces;
using cse3902.Projectiles;
using cse3902.Sounds;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Entities
{
    public class BoggusBossStateMachine : IEntityStateMachine
    {
        private BoggusBossSprite boggusBossSprite;
        private SpriteBatch spriteBatch;

        private ISprite fireball1;
        private ISprite fireball2;
        private ISprite fireball3;

        private bool isAttacking;
        private float fireballCounter;
        private const float fireballDelay = 2.5f;
        private bool isCoupling;

        private Vector2 center;

        public BoggusBossStateMachine(BoggusBossSprite boggusBossSprite, SpriteBatch spriteBatch, Vector2 center)
        {
            this.boggusBossSprite = boggusBossSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = true;
            fireballCounter = 0;
            this.isCoupling = true;

            this.center = center;
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
            location.Y = this.center.Y - 10;

            direction1 = new Vector2(-1, 0);
            direction2 = new Vector2(-3, 1);
            direction2.Normalize();
            direction3 = new Vector2(-3, -1);
            direction3.Normalize();

            location.X += -15; //originate fireballs at mouth if facing left

            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            fireball1 = projectileHandler.CreateFireballObject(spriteBatch, location, direction1);
            fireball2 = projectileHandler.CreateFireballObject(spriteBatch, location, direction2);
            fireball3 = projectileHandler.CreateFireballObject(spriteBatch, location, direction3);

        }

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection == new Vector2(0, 0))
            {
                //direction vector of (0,0) indicates just reverse the current direction
                if (boggusBossSprite.StartingFrameIndex == (int)BoggusBossSprite.FrameIndex.RightFacing)
                {
                    boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.LeftFacing;
                }
                else
                {
                    boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.RightFacing;
                }
                return;
            }

            if (newDirection.X > 0)
            {
                boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.RightFacing;
            }
            else
            {
                boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.LeftFacing;
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
            boggusBossSprite.Draw();
        }

        public void Die()
        {
        }

        public void Update(GameTime gameTime, Vector2 center, Boolean pauseAnim)
        {
            this.center = center;
            if (fireballCounter > fireballDelay)
            {
                LoadFireballs();
                if (this.isAttacking)
                {
                    PlaySound();
                    this.isAttacking = false;
                } else
                {
                    this.isAttacking = true;
                }
                fireballCounter = 0;
            }
            else
            {
                fireballCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (!pauseAnim) boggusBossSprite.Update(gameTime);
        }

        public bool IsAttacking
        {
            get => isAttacking;
            set
            {
                isAttacking = value;
            }
        }

        private void PlaySound()
        {
            if (this.isCoupling)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.highercoupling);
                this.isCoupling = false;
            } else
            {
                SoundFactory.PlaySound(SoundFactory.Instance.lowcohesion);
                this.isCoupling = true;
            }
            
        }
    }
}
