using System;
using Microsoft.Xna.Framework;

namespace cse3902.Collision
{
    public interface ICollidable
    {
        void OnCollidedWith(ICollidable collidableObject);
        ref Rectangle RectangleRef { get; }
        int DamageValue { get; }
        void ResetCollisions();
    }
}