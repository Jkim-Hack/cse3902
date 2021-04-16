using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Input;
using cse3902.Projectiles;

namespace cse3902.ParticleSystem
{
    public class ParticleEngine
    {
        private static ParticleEngine instance = new ParticleEngine();
        public static ParticleEngine Instance { get => instance; }

        private List<IParticleEmmiter> emitters;

        private Texture2D circle;
        private Texture2D glow;
        private Texture2D cloud;
        private Texture2D diamond;
        private Texture2D star;
        private Texture2D ring;

        private bool pressed = false;

        private ParticleEngine()
        {
            emitters = new List<IParticleEmmiter>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            circle = content.Load<Texture2D>("particles/circle");
            glow = content.Load<Texture2D>("particles/glow");
            cloud = content.Load<Texture2D>("particles/cloud");
            diamond = content.Load<Texture2D>("particles/diamond");
            star = content.Load<Texture2D>("particles/star");
            ring = content.Load<Texture2D>("particles/ring");
        }

        public void CreateArrowEffect(Vector2 origin)
        {
            emitters.Add(new ArrowEmitter(cloud, origin));
        }

        public void CreateSwordEffect(Vector2 origin)
        {
            emitters.Add(new SwordEmitter(star, origin));
        }

        public void CreateBombEffect(Vector2 origin)
        {
            emitters.Add(new BombEmitter(glow, origin));
        }

        public IDependentParticleEmmiter CreateFireballEffect(IProjectile fireball)
        {
            IDependentParticleEmmiter fireballEmitter = new FireballEmitter(glow, fireball);
            emitters.Add(fireballEmitter);
            return fireballEmitter;
        }

        public void Update(GameTime gameTime)
        {
            emitters.ForEach(emitter => emitter.Update(gameTime));
            emitters.RemoveAll(emitter => emitter.AnimationDone);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && !pressed)
            {
                ProjectileHandler.Instance.CreateBombItem(spriteBatch, new Vector2(650, 970));
                pressed = true;
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift)) pressed = false;

            emitters.ForEach(emitter => emitter.Draw(spriteBatch, transformationMatrix));
        }

        public bool UseParticleEffects
        {
            get => ParticleConstants.UseParticleEffects;
        }
    }
}
