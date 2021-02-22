using System;
namespace cse3902.Interfaces
{
    public interface IProjectile: ISprite
    {
        public bool AnimationComplete { get; set; }
    }
}
