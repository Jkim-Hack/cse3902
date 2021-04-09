using cse3902.Interfaces;
using System;

namespace cse3902.ParticleEngine
{
    public class ParticleEngine
    {
        private static ParticleEngine instance = new ParticleEngine();
        public static ParticleEngine Instance { get => instance; }

        private ParticleEngine() {}

        public void LoadAllTextures()
        {

        }
    }
}
