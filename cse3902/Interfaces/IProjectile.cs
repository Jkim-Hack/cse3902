using System;
using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IProjectile: ISprite
    {
        public bool AnimationComplete { get; set; }
        public ICollidable Collidable { get; }
        public Vector2 Direction { get; set; }
        public int Damage { get; }
    }
}
