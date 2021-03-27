using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Doors;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class DoorCollidable : ICollidable
    {
        private IDoor door;
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public bool DamageDisabled { get; set; }

        public DoorCollidable(IDoor door)
        {
            this.door = door;
            DamageDisabled = true;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

            if (collidableObject is PlayerCollidable)
            {
                IDoor.DoorState state = this.door.State;
                if (state == IDoor.DoorState.Open)
                {
                    if (this.door is NormalUpDoor && collidableObject.RectangleRef.Y < this.RectangleRef.Y)
                    {
                        this.door.Interact();
                    }
                    else if (this.door is NormalDownDoor && collidableObject.RectangleRef.Y + collidableObject.RectangleRef.Height > this.RectangleRef.Y + this.RectangleRef.Height)
                    {
                        this.door.Interact();
                    }
                    else if (this.door is NormalLeftDoor && collidableObject.RectangleRef.X < this.RectangleRef.X)
                    {
                        this.door.Interact();
                    }
                    else if (this.door is NormalRightDoor && collidableObject.RectangleRef.X + collidableObject.RectangleRef.Width > this.RectangleRef.X + this.RectangleRef.Width)
                    {
                        this.door.Interact();
                    }
                }
                else
                {
                    this.door.Interact();
                }
            } 
            else if (collidableObject is ProjectileCollidable) //needs to be modified to only allow bomb projectile to do this
            {
                if (this.door.State == IDoor.DoorState.Wall && this.door.ConnectedDoor != null)
                {
                    this.door.State = IDoor.DoorState.Bombed;
                    this.door.ConnectedDoor.State = IDoor.DoorState.Bombed;
                }
            }
        }




        public ref Rectangle RectangleRef
        {
            get => ref door.Bounds;
        }



        public int DamageValue
        {
            get => 0;
        }

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Capacity; i++)
            {
                collisionOccurrences[i] = false;
            }
        }

        public IDoor.DoorState State
        {
            get => this.door.State;
        }
    }
}
