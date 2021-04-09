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

        public ArrowEmitter(Texture2D particle, Vector2 location)
        {
            this.particle = particle;
            this.location = location;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw()
        {

        }

        public bool AnimationDone
        {
            get => false;
        }
    }
}
