using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class SwordEmitter : IParticleEmmiter
    {
        private Texture2D texture;
        private Vector2 origin;

        private List<IParticle> particles;

        private int particleAddAmount;

        public SwordEmitter(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;

            this.particles = new List<IParticle>();

            this.particleAddAmount = ParticleConstants.ArrowParticleAddAmmount;

            GenerateParticles(ParticleConstants.ArrowParticleInitialAmount);
        }

        private void GenerateParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.ArrowParticleLifetimeMin, ParticleConstants.ArrowParticleLifetimeMax);

                int colorVal = rand.Next(ParticleConstants.ArrowParticleColorMin, ParticleConstants.ArrowParticleColorMax);
                float colorOpacity = (float)rand.NextDouble();
                Color color = new Color(colorVal, colorVal, colorVal) * colorOpacity;

                particles.Add(new StraightMovingParticle(texture, color, origin, GetRandomVelocity(rand), lifeTime, ParticleConstants.ArrowParticleSize));
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
            if (particleAddAmount > 0)
            {
                GenerateParticles(particleAddAmount);
                particleAddAmount--;
            }

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
            get => particles.Count == 0 && particleAddAmount == 0;
        }
    }
}
