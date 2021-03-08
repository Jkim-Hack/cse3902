using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

        // Bounds[0] = Room transition collidable rectangle
        // Rest are normal hitboxes
        public List<Rectangle> Bounds { get; }
        public void Interact();
        public void Draw();
    }
}
