using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface IDoor : ICollidableItemEntity
    {
        public enum DoorState
        {
            Open,
            Closed,
            Locked,
            Wall,
            Bombed,
            None
        }

        public ref Rectangle Bounds { get; }
        public IDoor ConnectedDoor { set; }
        public void Interact();
        public Vector2 PlayerReleasePosition();
        public Vector2 PlayerReleaseDirection();
        public void Draw();
        public DoorState State { get; set; }
        public void Reset();
    }
}
