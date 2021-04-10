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
        private Vector2 origin;

        private List<IParticle> particles;

        public ArrowEmitter(Texture2D texture, Vector2 origin)
        {
            Console.WriteLine(origin);
            this.texture = texture;
            this.origin = origin;

            this.particles = new List<IParticle>();

            GenerateParticles();
        }

        private void GenerateParticles()
        {
            for (int i = 0; i < ParticleConstants.ArrowParticleDensity; i++)
            {
                Random rand = ParticleConstants.rand;

                Vector2 center = new Vector2((DimensionConstants.OriginalWindowWidth) * 2 + 100, (DimensionConstants.GameplayHeight / 3) * 5 + 100); // testing

                int lifeTime = rand.Next(ParticleConstants.ArrowParticleLifetimeMin, ParticleConstants.ArrowParticleLifetimeMax);

                int colorVal = rand.Next(ParticleConstants.ArrowParticleColorMin, ParticleConstants.ArrowParticleColorMax);
                float colorOpacity = (float)rand.NextDouble();
                Color color = new Color(colorVal, colorVal, colorVal) * colorOpacity;

                particles.Add(new StraightMovingParticle(texture, color, center, GetRandomVelocity(rand), lifeTime, ParticleConstants.ArrowParticleSize));
            }
        }

        private Vector2 GetRandomVelocity(Random rand)
        {
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());

            if (rand.NextDouble() >= 0.5) velocity.X *= -1;
            if (rand.NextDouble() >= 0.5) velocity.Y *= -1;

            /* Gives effect an overall circular shape */
            if (rand.NextDouble() >= 0.75) velocity.Normalize();

            velocity *= ParticleConstants.ArrowParticleVelocityScale;

            return velocity;
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
            get => particles.Count == 0;
        }
    }
}
