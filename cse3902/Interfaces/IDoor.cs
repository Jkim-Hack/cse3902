using Microsoft.Xna.Framework;
using System.Collections.Generic;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IDoor
    {
        public enum DoorState
        {
            Open,
            Closed,
            Locked,
            Wall
        }
        public ICollidable collidable { get; }

        // Bounds[0] = Room transition collidable rectangle
        // Rest are normal hitboxes
        public ref List<Rectangle> Bounds { get; }
        public void Interact();
        public void Draw();

    }
}
