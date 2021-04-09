using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class StraightMovingParticle : IParticle
    {
        private Texture2D particle;
        private Vector2 origin;
        private Vector2 velocity;
        private int lifeTime;

        public StraightMovingParticle(Texture2D particle, Vector2 origin, Vector2 velocity, int lifeTime)
        {
            this.particle = particle;
            this.origin = origin;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
        }

        public void Update(GameTime gameTime)
        {
            lifeTime--;
            origin += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(particle, new Rectangle(100, 100, 50, 50), null, Color.White, 0, new Vector2(), SpriteEffects.None, 0);
        }

        public bool Dead
        {
            get => lifeTime == 0;
        }
    }
}
