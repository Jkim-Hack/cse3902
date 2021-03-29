using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Rooms;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class TrapCollidable : ICollidable
    {
        private IEntity trap;
        private Boolean[] collisionOccurrences;
        private int damage;

        public bool DamageDisabled { get; set; }

        public TrapCollidable(IEntity trap, int damage)
        {
            this.trap = trap;
            this.damage = damage;
            DamageDisabled = true;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //if the trap has been 'triggered', its collisions will be different
            //might want trap to have some property that keeps track of
            //whether it has been triggered or not
            if (collidableObject is TrapCollidable || collidableObject is WallCollidable)
            {

            }
        }

        public ref Rectangle RectangleRef
        {
            get => ref trap.Bounds;
        }

        public int DamageValue
        {
            get => this.damage;
        }

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Length; i++)
            {
                collisionOccurrences[i] = false;
            }
        }
    }

}
