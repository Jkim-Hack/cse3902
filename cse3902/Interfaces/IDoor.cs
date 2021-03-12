using Microsoft.Xna.Framework;

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

        public ref Rectangle Bounds { get; }
        public IDoor ConnectedDoor { set; }
        public void Interact();
        public Vector2 PlayerReleasePosition();
        public Vector2 PlayerReleaseDirection();
        public void Draw();
    }
}
