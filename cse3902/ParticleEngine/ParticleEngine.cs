using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleEngine
{
    public class ParticleEngine
    {
        public enum ParticleEmitter
        {
            ArrowHit
        }

        private static ParticleEngine instance = new ParticleEngine();
        public static ParticleEngine Instance { get => instance; }

        List<IParticleEmmiter> emitters;

        private ParticleEngine()
        {
            emitters = new List<IParticleEmmiter>();
        }

        public void LoadAllTextures()
        {

        }

        public void CreateNewEmitter(ParticleEmitter emitter)
        {
            switch (emitter)
            {
                case ParticleEmitter.ArrowHit:
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (IParticleEmmiter emitter in emitters) emitter.Update(gameTime);

            /* Remove all emitters that have completed their animation */
            emitters.RemoveAll(emitter => emitter.AnimationDone);
        }

        public void Draw()
        {
            foreach (IParticleEmmiter emitter in emitters) emitter.Draw();
        }
    }
}
