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


        public DoorCollidable(IDoor door)
        {
            this.door = door;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

            if (collidableObject is PlayerCollidable)
            {
                if (this.door is NormalUpDoor)
                {
                    if (collidableObject.RectangleRef.Y < this.RectangleRef.Y)
                    {
                        this.door.Interact();
                    }
                }
                else if (this.door is NormalDownDoor)
                {
                    if (collidableObject.RectangleRef.Y > this.RectangleRef.Y)
                    {
                        this.door.Interact();
                    }
                }
                else if (this.door is NormalLeftDoor)
                {
                    if (collidableObject.RectangleRef.X < this.RectangleRef.X)
                    {
                        this.door.Interact();
                    }
                }
                else if (this.door is NormalRightDoor)
                {
                    if (collidableObject.RectangleRef.X > this.RectangleRef.X)
                    {
                        this.door.Interact();
                    }
                }
                else
                {
                    //it's a staircase
                    //todo: make sure this is the correct direction for the threshold
                    if (collidableObject.RectangleRef.Y > this.RectangleRef.Y)
                    {
                        this.door.Interact();
                    }
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
