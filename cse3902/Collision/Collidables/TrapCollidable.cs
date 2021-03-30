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
        private ITrap trap;
        private Boolean[] collisionOccurrences;

        public bool DamageDisabled { get; set; }

        public TrapCollidable(ITrap trap)
        {
            this.trap = trap;
            DamageDisabled = true;
            ResetCollisions();
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //if the trap has been 'triggered', its collisions will be different
            //might want trap to have some property that keeps track of
            //whether it has been triggered or not
            if (collidableObject is TrapCollidable || collidableObject is WallCollidable)
            {
                if (this.trap.IsTriggered)
                {
                    this.trap.Direction = -this.trap.Direction;
                    this.trap.Center = this.trap.PreviousCenter;
                }
            }

            if (collidableObject is PlayerCollidable)
            {
                if (!this.trap.IsTriggered)
                {
                    this.trap.Trigger();
                }
            }
        }

        public ref Rectangle RectangleRef
        {
            get => ref trap.Bounds;
        }

        public int DamageValue
        {
            get => this.trap.Damage;
        }

        public ITrap Trap
        {
            get => this.trap;
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
