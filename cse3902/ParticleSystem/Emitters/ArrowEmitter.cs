using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class ArrowEmitter : IParticleEmmiter
    {
        private Texture2D particle;
        private Vector2 location;

        private bool animationDone;

        public ArrowEmitter(Texture2D particle, Vector2 location)
        {
            this.particle = particle;
            this.location = location;

            animationDone = false;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public bool AnimationDone
        {
            get => animationDone;
        }
    }
}
