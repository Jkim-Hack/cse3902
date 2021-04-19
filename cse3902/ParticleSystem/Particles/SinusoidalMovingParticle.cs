using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.ParticleSystem
{
    public class SinusoidalMovingParticle : IParticle
    {
        private Texture2D texture;
        private Color color;

        private Vector2 origin;
        private Vector2 velocity;

        private Vector2 amplitudeDirection;
        private int amplitude;
        private int amplitudeTravelled;

        private int lifeTime;

        private int size;

        public SinusoidalMovingParticle(Texture2D texture, Color color, Vector2 origin, Vector2 velocity, int amplitude, int lifeTime, int size)
        {
            this.texture = texture;
            this.color = color;

            this.origin = origin;
            this.velocity = velocity;

            /* Amplitude direction should be perpendicular to particle velocity */
            this.amplitudeDirection = new Vector2(velocity.Y, -velocity.X);
            this.amplitudeDirection.Normalize();

            this.amplitude = amplitude;
            this.amplitudeTravelled = 0;

            this.lifeTime = lifeTime;

            this.size = size;
        }

        public void Update(GameTime gameTime)
        {
            lifeTime--;

            origin += velocity;
            origin += amplitudeDirection;
            
            amplitudeTravelled++;
            if (amplitudeTravelled == amplitude)
            {
                amplitudeDirection *= -1;
                amplitudeTravelled = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)origin.X, (int)origin.Y, size, size), null, color, 0, new Vector2(), SpriteEffects.None, 0);
        }

        public bool Dead
        {
            get => lifeTime == 0;
        }
    }
}
