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
        public IDoor ConnectedDoor { set; }
        public void Interact();
        public Vector2 PlayerReleasePosition();
        public Vector2 PlayerReleaseDirection();
        public void Draw();
    }
}
