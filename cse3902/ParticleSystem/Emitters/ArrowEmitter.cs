using cse3902.Interfaces;
using cse3902.Constants;
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

            GenerateParticles();
        }

        private void GenerateParticles()
        {
            for (int i = 0; i < 10; i++)
            {
                particles.Add(new StraightMovingParticle(texture, new Vector2((DimensionConstants.OriginalWindowWidth) * 2, (DimensionConstants.GameplayHeight / 3) * 5), new Vector2(1, 1), 100));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (IParticle particle in particles) particle.Update(gameTime);

            /* Remove all dead particles */
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
