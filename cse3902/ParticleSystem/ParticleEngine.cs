using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace cse3902.ParticleSystem
{
    public class ParticleEngine
    {
        public enum ParticleEmitter
        {
            ArrowHit
        }

        private static ParticleEngine instance = new ParticleEngine();
        public static ParticleEngine Instance { get => instance; }

        private List<IParticleEmmiter> emitters;

        private Texture2D circle;
        private Texture2D cloud;
        private Texture2D diamond;
        private Texture2D star;
        private Texture2D ring;

        private bool pressed;

        private ParticleEngine()
        {
            emitters = new List<IParticleEmmiter>();
            pressed = false;
        }

        public void LoadAllTextures(ContentManager content)
        {
            circle = content.Load<Texture2D>("particles/circle");
            cloud = content.Load<Texture2D>("particles/cloud");
            diamond = content.Load<Texture2D>("particles/diamond");
            star = content.Load<Texture2D>("particles/star");
            ring = content.Load<Texture2D>("particles/ring");
        }

        public void CreateNewEmitter(ParticleEmitter emitter, Vector2 origin)
        {
            if (emitter == ParticleEmitter.ArrowHit) emitters.Add(new ArrowEmitter(circle, origin));
        }

        public void Update(GameTime gameTime)
        {
            foreach (IParticleEmmiter emitter in emitters) emitter.Update(gameTime);

            /* Remove all emitters that have completed their animation */
            emitters.RemoveAll(emitter => emitter.AnimationDone);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IParticleEmmiter emitter in emitters) emitter.Draw(spriteBatch);
        }

        public bool UseParticleEffects
        {
            get => ParticleConstants.UseParticleEffects;
        }
    }
}
