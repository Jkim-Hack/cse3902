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
    }
}
