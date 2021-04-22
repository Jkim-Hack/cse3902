using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Sprites;
using cse3902.Sounds;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Projectiles
{
    public class EnemyBoomerangProjectile : IProjectile
    {
        private BoomerangProjectile boomerang;

        public EnemyBoomerangProjectile(SpriteBatch batch, Texture2D texture, ISprite enemy, Vector2 dir)
        {
            boomerang = new BoomerangProjectile(batch, texture, enemy, dir);
        }

        public void Draw()
        {
            boomerang.Draw();
        }

        public int Update(GameTime gameTime)
        {

            return boomerang.Update(gameTime);
        }

        public ref Rectangle Box
        {
            get
            {
                return ref boomerang.Box;
            }
        }

        public Vector2 Center
        {
            get => boomerang.Center;
            set => boomerang.Center = value;
        }

        public Texture2D Texture
        {
            get => boomerang.Texture;
        }

        public bool AnimationComplete
        {
            get => boomerang.AnimationComplete;

            set => boomerang.AnimationComplete = value;
        }

        public int Damage
        {
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaBoomerang);
        }

        public Vector2 Direction
        {
            get => boomerang.Direction;
            set => boomerang.Direction = value;

        }

        public ICollidable Collidable
        {
            get => boomerang.Collidable;
        }

        public bool Collided
        {
            get => boomerang.Collided;
            set => boomerang.Collided = value;
        }
    }
}