using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class StraightMovingParticle : IParticle
    {
        private Texture2D particle;
        private Texture2D startingLocation;

        private bool deadParticle;

        public StraightMovingParticle()
        {
            deadParticle = false;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw()
        {

        }

        public bool Dead
        {
            get => deadParticle;
        }
    }
}
