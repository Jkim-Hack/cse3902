using cse3902.Constants;
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

        private bool isAttacking;
        private float fireballCounter;
        private const float fireballDelay = 2.5f;
        private bool isCoupling;

        private Vector2 center;

        private IPlayer player;

        public BoggusBossStateMachine(BoggusBossSprite boggusBossSprite, SpriteBatch spriteBatch, Vector2 center, IPlayer link)
        {
            this.boggusBossSprite = boggusBossSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = false;
            fireballCounter = 0;
            this.isCoupling = true;

            this.center = center;

            player = link;
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
            location.X += 3 * MovementConstants.AquamentusFireballChangeX;
            location.Y = this.center.Y - 10;

            direction2 = player.Center - location;
            direction2.Normalize();
            double angle2 = Math.Atan2(direction2.Y, direction2.X);
            double angle1 = angle2 + MovementConstants.BoggusFireballSpreadAngle;
            double angle3 = angle2 - MovementConstants.BoggusFireballSpreadAngle;
            direction1 = new Vector2((float)Math.Cos(angle1), (float)Math.Sin(angle1));
            direction3 = new Vector2((float)Math.Cos(angle3), (float)Math.Sin(angle3));

            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateFireballObject(spriteBatch, location, direction1);
            projectileHandler.CreateFireballObject(spriteBatch, location, direction2);
            projectileHandler.CreateFireballObject(spriteBatch, location, direction3);

        }

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {

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
            if (fireballCounter >= fireballDelay / 2)
            {
                this.boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.MouthClosed;
            }
            if (fireballCounter > fireballDelay)
            {
                LoadFireballs();
                this.boggusBossSprite.StartingFrameIndex = (int)BoggusBossSprite.FrameIndex.MouthOpen;
                if (!this.isAttacking)
                {
                    PlaySound();
                    this.isAttacking = true;
                } else
                {
                    this.isAttacking = false;
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
