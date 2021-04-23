using cse3902.Interfaces;
using cse3902.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Projectiles
{
    public class EnemyBoomerangProjectile : IProjectile
    {
        private BoomerangProjectile boomerang;

        public EnemyBoomerangProjectile(SpriteBatch batch, Texture2D texture, ISprite enemy, Vector2 dir, Game1 game, int travelDistance)
        {
            boomerang = new BoomerangProjectile(batch, texture, enemy, dir, game, travelDistance);
        }

        public void Draw()
        {
            boomerang.Draw();
        }

        public int Update(GameTime gameTime)
        {

            return boomerang.Update(gameTime);
        }

        public void ReverseDirectionIfNecessary()
        {
            boomerang.ReverseDirectionIfNecessary();
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