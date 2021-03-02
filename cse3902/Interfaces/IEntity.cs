using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IEntity
    {
        public ref Rectangle Bounds { get; }
        public void Attack();
        public void ChangeDirection(Vector2 direction);
        public void TakeDamage(int damage);
        public void Die();
        public void Update(GameTime gameTime);
        public void Draw();
        public void BeShoved();
    }
}
