using System;
using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IEntity : ICollidableItemEntity
    {
        public enum EnemyType
        {
            A,
            B,
            C,
            D,
            X
        }
        public ref Rectangle Bounds { get; }
        public int Health { get; }
        public Vector2 Direction { get; }
        public Vector2 Center { get; set; }
        public Vector2 PreviousCenter { get; }
        public EnemyType Type { get; }
        public (bool Stun, float StunDuration) Stunned { get; set; }

        public void Attack();
        public void ChangeDirection(Vector2 direction);
        public void TakeDamage(int damage);
        public void Die();
        public void Update(GameTime gameTime);
        public void Draw();
        public void BeShoved();
        public void StopShove();
        public IEntity Duplicate();
    }
}
