using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class EnemyDeathEmitter : IParticleEmmiter
    {
        private Texture2D texture;
        private Vector2 origin;

        private List<IParticle> particles;

        private int particleAddAmount;

        public EnemyDeathEmitter(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;

            this.particles = new List<IParticle>();

            this.particleAddAmount = 50;

            GenerateParticles(50);
        }

        private void GenerateParticles(int num)
        {
            Random rand = ParticleConstants.rand;

            for (int i = 0; i < num; i++)
            {
                int lifeTime = rand.Next(20, 40);

                int colorVal = rand.Next(150, 256);
                float colorOpacity = (float)rand.NextDouble();
                Color color = new Color(colorVal, colorVal, colorVal) * colorOpacity;

                Vector2 velocity = GetRandomVelocity(rand);

                particles.Add(new StraightMovingParticle(texture, color, origin, velocity, lifeTime, 10));
            }
        }

        private Vector2 GetRandomVelocity(Random rand)
        {
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());

            if (rand.NextDouble() >= 0.5) velocity.X *= -1;
            if (rand.NextDouble() >= 0.5) velocity.Y *= -1;

            /* Gives effect an overall circular shape */
            if (rand.NextDouble() >= 0.75) velocity.Normalize();

            velocity *= 0.4f;

            return velocity;
        }

        public void Update(GameTime gameTime)
        {
            if (particleAddAmount > 0)
            {
                GenerateParticles(particleAddAmount);
                particleAddAmount--;
            }

            particles.ForEach(particle => particle.Update(gameTime));
            particles.RemoveAll(particle => particle.Dead);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformationMatrix);
            particles.ForEach(particle => particle.Draw(spriteBatch));
            spriteBatch.End();
        }

        public bool AnimationDone
        {
            get => particles.Count == 0 && particleAddAmount == 0;
        }
    }
}
