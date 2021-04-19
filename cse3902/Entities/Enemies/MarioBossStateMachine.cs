using cse3902.Interfaces;
using cse3902.Projectiles;
using cse3902.Sounds;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Entities
{
    public class MarioBossStateMachine : IEntityStateMachine
    {
        private MarioBossSprite marioBossSprite;
        private SpriteBatch spriteBatch;

        private ISprite fireball1;
        private ISprite fireball2;
        private ISprite fireball3;

        private bool isAttacking;
        private float fireballCounter;
        private const float fireballDelay = 2.5f;

        private Vector2 center;

        public MarioBossStateMachine(MarioBossSprite marioBossSprite, SpriteBatch spriteBatch, Vector2 center)
        {
            this.marioBossSprite = marioBossSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = true;
            fireballCounter = 0;

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

            if (marioBossSprite.StartingFrameIndex == (int)MarioBossSprite.FrameIndex.RightFacing)
            {
                direction1 = new Vector2(1, 0);
                direction2 = new Vector2(3, 1);
                direction2.Normalize();
                direction3 = new Vector2(3, -1);
                direction3.Normalize();
                location.X += 15;
            }
            else
            {
                direction1 = new Vector2(-1, 0);
                direction2 = new Vector2(-3, 1);
                direction2.Normalize();
                direction3 = new Vector2(-3, -1);
                direction3.Normalize();

                location.X += -15; //originate fireballs at mouth if facing left
            }

            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            fireball1 = projectileHandler.CreateFireballObject(spriteBatch, location, direction1);
            fireball2 = projectileHandler.CreateFireballObject(spriteBatch, location, direction2);
            fireball3 = projectileHandler.CreateFireballObject(spriteBatch, location, direction3);

            // correct location for this?
            SoundFactory.PlaySound(SoundFactory.Instance.bossScream);
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
                if (marioBossSprite.StartingFrameIndex == (int)MarioBossSprite.FrameIndex.RightFacing)
                {
                    marioBossSprite.StartingFrameIndex = (int)MarioBossSprite.FrameIndex.LeftFacing;
                }
                else
                {
                    marioBossSprite.StartingFrameIndex = (int)MarioBossSprite.FrameIndex.RightFacing;
                }
                return;
            }

            if (newDirection.X > 0)
            {
                marioBossSprite.StartingFrameIndex = (int)MarioBossSprite.FrameIndex.RightFacing;
            }
            else
            {
                marioBossSprite.StartingFrameIndex = (int)MarioBossSprite.FrameIndex.LeftFacing;
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
            marioBossSprite.Draw();
        }

        public void Die()
        {
        }

        public void Update(GameTime gameTime, Vector2 center, Boolean pauseAnim)
        {
            this.center = center;
            if (this.IsAttacking)
            {
                if (fireballCounter > fireballDelay)
                {
                    LoadFireballs();
                    fireballCounter = 0;
                }
                else
                {
                    fireballCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (!pauseAnim) marioBossSprite.Update(gameTime);
        }

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
