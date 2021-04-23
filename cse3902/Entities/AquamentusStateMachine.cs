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
    public class AquamentusStateMachine : IEntityStateMachine
    {
        private AquamentusSprite aquamentusSprite;
        private SpriteBatch spriteBatch;

        private bool isAttacking;
        private float fireballCounter;  
        private const float fireballDelay = 2.5f;
        
        private Vector2 center;

        private IPlayer player;

        public AquamentusStateMachine(AquamentusSprite aquamentusSprite, SpriteBatch spriteBatch, Vector2 center, IPlayer link)
        {
            this.aquamentusSprite = aquamentusSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = true;
            fireballCounter = 0;

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
            location.X -= MovementConstants.AquamentusFireballChangeX;
            if (aquamentusSprite.StartingFrameIndex == (int)AquamentusSprite.FrameIndex.RightFacing) location.X += 2 * MovementConstants.AquamentusFireballChangeX;
            location.Y = this.center.Y - 10;

            direction2 = player.Center - location;
            direction2.Normalize();
            double angle2 = Math.Atan2(direction2.Y, direction2.X);
            double angle1 = angle2 + MovementConstants.AquamentusFireballSpreadAngle;
            double angle3 = angle2 - MovementConstants.AquamentusFireballSpreadAngle;
            direction1 = new Vector2((float)Math.Cos(angle1), (float)Math.Sin(angle1));
            direction3 = new Vector2((float)Math.Cos(angle3), (float)Math.Sin(angle3));

            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateFireballObject(spriteBatch, location, direction1);
            projectileHandler.CreateFireballObject(spriteBatch, location, direction2);
            projectileHandler.CreateFireballObject(spriteBatch, location, direction3);

            SoundFactory.PlaySound(SoundFactory.Instance.bossScream);
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection == new Vector2(0,0))
            {
                //direction vector of (0,0) indicates just reverse the current direction
                if (aquamentusSprite.StartingFrameIndex == (int)AquamentusSprite.FrameIndex.RightFacing)
                {
                    aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.LeftFacing;
                } else
                {
                    aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.RightFacing;
                }
                return;
            }

            if (newDirection.X > 0)
            {
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.RightFacing;
            }
            else
            {
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.LeftFacing;
            }
        }

        public void Attack()
        {
            this.IsAttacking = true;
        }

        public void Draw()
        {
            aquamentusSprite.Draw();
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

            if (!pauseAnim) aquamentusSprite.Update(gameTime);
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