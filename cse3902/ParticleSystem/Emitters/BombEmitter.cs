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

        private int particleAddAmountCenter;

        public BombEmitter(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;

            this.particles = new List<IParticle>();

            this.particleAddAmountCenter = ParticleConstants.BombParticleCenterAddAmount;

            GenerateCenterParticles(ParticleConstants.BombParticleCenterAmount);
            GenerateRingParticles(ParticleConstants.BombParticleRingAmount);
        }

        private void GenerateCenterParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.BombParticleCenterLifetimeMin, ParticleConstants.BombParticleCenterLifetimeMax);

                float colorOpacity = (float)rand.NextDouble();
                Color color = new Color(255, rand.Next(ParticleConstants.BombParticleCenterColorMax), 0) * colorOpacity;

                Vector2 velocity = GetRandomVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, origin, velocity, lifeTime, ParticleConstants.BombParticleCenterSize));
            }
        }

        private void GenerateRingParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.BombParticleRingLifetimeMin, ParticleConstants.BombParticleRingLifetimeMax);

                float colorOpacity = (float)rand.NextDouble();
                Color color = Color.White * colorOpacity;

                Vector2 velocity = GetRandomVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, origin, velocity, lifeTime, ParticleConstants.BombParticleRingSize));
            }
        }

        private Vector2 GetRandomVelocity(Random rand)
        {
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());

            if (rand.NextDouble() >= 0.5) velocity.X *= -1;
            if (rand.NextDouble() >= 0.5) velocity.Y *= -1;

            /* Gives effect a circular shape */
            velocity.Normalize();

            velocity *= ParticleConstants.BombParticleCenterVelocityScale;

            return velocity;
        }

        public void Update(GameTime gameTime)
        {
            if (particleAddAmountCenter > 0)
            {
                GenerateCenterParticles(particleAddAmountCenter);
                particleAddAmountCenter--;
            }

            particles.ForEach(particle => particle.Update(gameTime));
            particles.RemoveAll(particle => particle.Dead);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, SamplerState.PointClamp, null, null, null, transformationMatrix);
            particles.ForEach(particle => particle.Draw(spriteBatch));
            spriteBatch.End();
        }

        public bool AnimationDone
        {
            get => particles.Count == 0 && particleAddAmountCenter == 0;
        }
    }
}
