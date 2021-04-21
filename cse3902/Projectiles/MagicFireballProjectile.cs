using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.ParticleSystem;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Projectiles
{
    public class MagicFireballProjectile : IProjectile
    {
        Fireball fireball;
        ICollidable collidable;
        Rectangle box;

        public MagicFireballProjectile(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)
        {
            fireball = new Fireball(spriteBatch, texture, startingPosition, direction);
            fireball.Speed = ItemConstants.MagicFireballSpeed;
            collidable = new ProjectileCollidable(this);
        }

        public Vector2 Center
        {
            get => fireball.Center;
            set
            {
                fireball.Center = value;
            }
        }

        public Texture2D Texture
        {
            get => fireball.Texture;
        }

        public int Update(GameTime gameTime)
        {
            return fireball.Update(gameTime);
        }

        public void Draw()
        {
            fireball.Draw();
        }

        public void KillParticles()
        {
            fireball.KillParticles();
        }

        public ref Rectangle Box
        {
            get
            {
                box = fireball.Box;
                box.Inflate(-box.Width/ ItemConstants.MagicFireballCollision, -box.Height / ItemConstants.MagicFireballCollision);
                return ref box;
            }
        }

        public bool AnimationComplete
        {
            get => fireball.AnimationComplete;
            set => fireball.AnimationComplete = value;
        }

        public void Erase()
        {
            fireball.Erase();
        }

        public int Damage
        {
            get => DamageConstants.MagicFireballDamage;
        }

        public Vector2 Direction
        {
            get => fireball.Direction;
            set => fireball.Direction = value;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public bool Collided
        {
            get => fireball.Collided;
            set => fireball.Collided = value;
        }
    }
}