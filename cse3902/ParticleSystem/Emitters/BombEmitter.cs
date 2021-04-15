using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class BombEmitter : IParticleEmmiter
    {
        private Texture2D texture;
        private Vector2 origin;

        private List<IParticle> particles;

        private int particleAddAmount;

        public BombEmitter(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;

            this.particles = new List<IParticle>();

            this.particleAddAmount = ParticleConstants.SwordParticleAddAmount;

            GenerateParticles(ParticleConstants.SwordParticleCircleAmount);
        }

        private void GenerateParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.SwordParticleLifetimeMin, ParticleConstants.SwordParticleLifetimeMax);

                float colorOpacity = (float)rand.NextDouble() * ParticleConstants.SwordParticleOpacity;
                Color color = new Color(255, 255, 255) * colorOpacity;

                Vector2 velocity = GetRandomVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, origin, velocity, lifeTime, ParticleConstants.SwordParticleSize));
            }
        }

        private Vector2 GetRandomVelocity(Random rand)
        {
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());

            if (rand.NextDouble() >= 0.5) velocity.X *= -1;
            if (rand.NextDouble() >= 0.5) velocity.Y *= -1;

            /* Gives effect a circular shape */
            velocity.Normalize();

            velocity *= ParticleConstants.SwordParticleVelocityScale;

            return velocity;
        }

        public void Update(GameTime gameTime)
        {
            if (particleAddAmount > 0)
            {
                GenerateParticles(particleAddAmount);
                particleAddAmount--;
            }

            foreach (IParticle particle in particles) particle.Update(gameTime);

            /* Remove all dead particles */
            particles.RemoveAll(particle => particle.Dead);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformationMatrix);
            foreach (IParticle particle in particles) particle.Draw(spriteBatch);
            spriteBatch.End();
        }

        public bool AnimationDone
        {
            get => particles.Count == 0 && particleAddAmount == 0;
        }
    }
}
