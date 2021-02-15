using System;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    public interface IEntity
    {
        public Rectangle Bounds { get; }
        public void Attack();
        public void ChangeDirection(Vector2 direction);
        public void TakeDamage();
        public void Die();
        public void Update(GameTime gameTime);
        public void Draw();
    }
}
