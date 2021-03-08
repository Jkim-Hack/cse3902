using System;
using cse3902.Collision;
namespace cse3902.Interfaces
{
    public interface IProjectile: ISprite
    {
        public bool AnimationComplete { get; set; }
        public ICollidable Collidable { get; }
        public int Damage { get; }
    }
}
