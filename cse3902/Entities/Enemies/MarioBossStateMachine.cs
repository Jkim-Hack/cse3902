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

        private bool isAttacking;
        private float fireballCounter;
        private const float fireballDelay = 2.5f;
        private bool isIntro;

        private Vector2 center;

        private IPlayer player;

        public MarioBossStateMachine(MarioBossSprite marioBossSprite, SpriteBatch spriteBatch, Vector2 center, IPlayer link)
        {
            this.marioBossSprite = marioBossSprite;
            this.spriteBatch = spriteBatch;

            this.isAttacking = true;
            this.isIntro = true;
            fireballCounter = 0;

            this.center = center;

            player = link;
        }

        private void LoadFireballs()
        {
            //the direction the fireballs will travel
            Vector2 direction;

            //all fireballs originate in the mouth
            Vector2 location;
            location.X = this.center.X;
            location.Y = this.center.Y - 10;

            direction = player.Center - location;
            direction.Normalize();

            location.X += -15; //originate fireballs at mouth if facing left

            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateFireballObject(spriteBatch, location, direction);


        }

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            //mario doesn't change direction
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
            if(isIntro)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.mariogreeting, 0.2f);
                isIntro = false;
            }
            if (fireballCounter > fireballDelay)
            {
                LoadFireballs();
                fireballCounter = 0;
            }
            else
            {
                fireballCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
