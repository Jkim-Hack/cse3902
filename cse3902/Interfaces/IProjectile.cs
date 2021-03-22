using System;
using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IProjectile: ISprite, ICollidableItemEntity
    {
        public bool AnimationComplete { get; set; }
        public Vector2 Direction { get; set; }
        public int Damage { get; }
        //public void CollisionAnimation();
        public bool Collided { get; set; }

    }
}
