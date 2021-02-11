using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities.Enemies
{
    public class Aquamentus: IEntity
    {
        private AquamentusSprite aquamentusSprite;

        public Aquamentus()
        {
        }

        public Rectangle Bounds
        {
            get => throw new NotImplementedException();
            //get => aquamentusSprite.Texture.Bounds;
        }
        public void Attack()
        {

        }
        public void ChangeDirection(Vector2 direction)
        {

        }
        public void TakeDamage()
        {

        }
        public void Die()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
