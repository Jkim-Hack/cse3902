using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class ArrowEmitter : IParticleEmmiter
    {
        private Texture2D texture;
        private Vector2 location;

        private List<IParticle> particles;

        public ArrowEmitter(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;

            this.particles = new List<IParticle>();
            particles.Add(new StraightMovingParticle(texture, new Vector2(), new Vector2(), 100));
        }

        public void Update(GameTime gameTime)
        {
            foreach (IParticle particle in particles) particle.Update(gameTime);

            /* Remove all emitters that have completed their animation */
            particles.RemoveAll(particle => particle.Dead);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IParticle particle in particles) particle.Draw(spriteBatch);
        }

        public bool AnimationDone
        {
            get => false;
        }
    }
}
