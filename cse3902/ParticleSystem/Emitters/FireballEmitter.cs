using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Input;

namespace cse3902.ParticleSystem
{
    public class FireballEmitter : IDependentParticleEmmiter
    {
        private Texture2D texture;
        private IProjectile fireball;

        private float timeSinceLastSpawn;

        private List<IParticle> particles;
        private bool kill;

        public FireballEmitter(Texture2D texture, IProjectile fireball)
        {
            this.texture = texture;
            this.fireball = fireball;

            this.timeSinceLastSpawn = 0;

            this.particles = new List<IParticle>();
            this.kill = false;
        }

        private void GenerateCenterParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.FireballParticleLifetimeMin, ParticleConstants.FireballParticleLifetimeMax);

                Color color = new Color(255, rand.Next(128), 0);

                Vector2 velocity = GetRandomCenterVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, fireball.Center, velocity, lifeTime, ParticleConstants.FireballParticleCenterSize));
            }
        }

        private void GenerateTrailParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(ParticleConstants.FireballParticleLifetimeMin, ParticleConstants.FireballParticleLifetimeMax);

                float colorOpacity = (float)rand.NextDouble();
                Color color = new Color(255, rand.Next(128), 0) * colorOpacity;

                Vector2 velocity = GetRandomTrailVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, fireball.Center, velocity, lifeTime, ParticleConstants.FireballParticleTrailSize));
            }
        }

        private Vector2 GetRandomCenterVelocity(Random rand)
        {
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());

            if (rand.NextDouble() >= 0.5) velocity.X *= -1;
            if (rand.NextDouble() >= 0.5) velocity.Y *= -1;

            /* Gives effect a circular shape */
            velocity.Normalize();

            velocity *= ParticleConstants.ArrowParticleVelocityScale;

            return velocity;
        }

        private Vector2 GetRandomTrailVelocity(Random rand)
        {
            float angle = (float)(Math.Atan2(fireball.Direction.Y, fireball.Direction.X) + Math.PI) % (float)(2 * Math.PI);
            angle += (float)rand.NextDouble() * ParticleConstants.FireballParticleAngleRange - ParticleConstants.FireballParticleAngleRange / 2.0f;

            Vector2 velocity = new Vector2((float)(rand.NextDouble() * Math.Cos(angle)), (float)(rand.NextDouble() * Math.Sin(angle)));

            return velocity;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastSpawn > ParticleConstants.FireballParticleAddDelay)
            {
                GenerateCenterParticles(ParticleConstants.FireballParticleCenterAmount);
                GenerateTrailParticles(ParticleConstants.FireballParticleTrailAmount);
                timeSinceLastSpawn = 0;
            }

            foreach (IParticle particle in particles) particle.Update(gameTime);

            /* Remove all dead particles */
            particles.RemoveAll(particle => particle.Dead);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, SamplerState.PointClamp, null, null, null, transformationMatrix);
            foreach (IParticle particle in particles) particle.Draw(spriteBatch);
            spriteBatch.End();
        }

        public bool AnimationDone
        {
            get => particles.Count == 0 || kill;
        }

        public bool Kill
        {
            set => kill = value;
        }
    }
}
