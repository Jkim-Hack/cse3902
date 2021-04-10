using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Input; // testing

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
            switch (emitter)
            {
                case ParticleEmitter.ArrowHit:
                    break;

                default:
                    break;
            }
        }

        private void AddArrowEmitter()
        {
            emitters.Add(new ArrowEmitter(circle, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
        }

        public void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !pressed)
            {
                pressed = true;
                AddArrowEmitter();
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released) pressed = false;

            foreach (IParticleEmmiter emitter in emitters) emitter.Update(gameTime);

            /* Remove all emitters that have completed their animation */
            emitters.RemoveAll(emitter => emitter.AnimationDone);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IParticleEmmiter emitter in emitters) emitter.Draw(spriteBatch);
        }
    }
}
