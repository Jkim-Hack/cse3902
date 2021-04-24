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

        public bool DamageDisabled { get; set; }

        public TrapCollidable(ITrap trap)
        {
            this.trap = trap;
            DamageDisabled = true;

        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //if (collidableObject is TrapCollidable)
            //{
            //    if (this.trap.IsTriggered && ((TrapCollidable)collidableObject).Trap.IsTriggered)
            //    {
            //        this.trap.Direction = -this.trap.Direction;
            //        this.trap.Center = this.trap.PreviousCenter;
            //    }
            //}

            //if (collidableObject is WallCollidable)
            //{
            //    this.trap.Direction = new Vector2(0, 0);
            //}


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
        }
    }

}
