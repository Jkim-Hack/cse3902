using System;
using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IEntity : ICollidableItemEntity
    {
        public ref Rectangle Bounds { get; }
        public int Health { get; }
        public Vector2 Direction { get; }

        public void Attack();
        public void ChangeDirection(Vector2 direction);
        public void TakeDamage(int damage);
        public void Die();
        public void Update(GameTime gameTime);
        public void Draw();
        public void BeShoved();
    }
}
