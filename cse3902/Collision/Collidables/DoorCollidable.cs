using System;
using cse3902.Interfaces;
using cse3902.Doors;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class DoorCollidable : ICollidable
    {
        private IDoor door;


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
                    if (collidableObject.RectangleRef.Y < this.door.Collidable.RectangleRef.Y)
                    {
                        //call room transition method
                    }
                } else if (this.door is NormalDownDoor)
                {
                    if (collidableObject.RectangleRef.Y > this.door.Collidable.RectangleRef.Y)
                    {
                        //call room transition method
                    }
                } else if (this.door is NormalLeftDoor)
                {
                    if (collidableObject.RectangleRef.X < this.door.Collidable.RectangleRef.X)
                    {
                        //call room transition method
                    }
                } else if (this.door is NormalRightDoor)
                {
                    if (collidableObject.RectangleRef.X > this.door.Collidable.RectangleRef.X)
                    {
                        //call room transition method
                    }
                } else
                {
                    //it's a staircase
                    //todo: make sure this is the correct direction for the threshold
                    if (collidableObject.RectangleRef.Y > this.door.Collidable.RectangleRef.Y)
                    {
                        //call room transition method
                    }
                }
            }
            else
            {
                //do nothing
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
    }
}
