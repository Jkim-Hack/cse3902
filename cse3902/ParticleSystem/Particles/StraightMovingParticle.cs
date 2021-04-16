using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.ParticleSystem
{
    public class StraightMovingParticle : IParticle
    {
        private Texture2D texture;
        private Color color;

        private Vector2 origin;
        private Vector2 velocity;

        private int lifeTime;

        private int size;

        public StraightMovingParticle(Texture2D texture, Color color, Vector2 origin, Vector2 velocity, int lifeTime, int size)
        {
            this.texture = texture;
            this.color = color;

            this.origin = origin;
            this.velocity = velocity;

            this.lifeTime = lifeTime;

            this.size = size;
        }

        public void Update(GameTime gameTime)
        {
            lifeTime--;
            origin += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)origin.X, (int)origin.Y, size, size), null, color, 0, new Vector2(), SpriteEffects.None, 0.5f);
        }

        public bool Dead
        {
            get => lifeTime == 0;
        }
    }
}
