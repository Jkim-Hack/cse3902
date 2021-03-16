using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Doors
{
    public class OffscreenUpDoor : IDoor
    {
        private Game1 game;
        private Vector3 roomTranslationVector;
        private Vector2 centerPosition;
        private IDoor connectedDoor;
        private Rectangle dest;
        private ICollidable collidable;

        public OffscreenUpDoor(Game1 game, Vector2 center)
        {
            this.game = game;
            roomTranslationVector = new Vector3(0, 0, 1);
            centerPosition = center;

            this.collidable = new DoorCollidable(this);
        }

        public void Interact()
        {
            game.RoomHandler.LoadNewRoom(game.RoomHandler.currentRoom + roomTranslationVector, connectedDoor);
        }
        public Vector2 PlayerReleasePosition()
        {
            return centerPosition + new Vector2(0, 16);
        }
        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, 32);
        }
        public void Draw()
        {
            //offscreen so nothing to draw
        }
        public IDoor.DoorState State
        {
            get
            {
                return IDoor.DoorState.None;
            }
            set
            {

            }
        }
        public ref Rectangle Bounds
        {
            get
            {
                Rectangle destination = new Rectangle((int)centerPosition.X, (int)centerPosition.Y, 16, 16);
                destination.Offset(-destination.Width / 2, -destination.Height / 2);
                dest = destination;

                return ref dest;
            }
        }
        public IDoor ConnectedDoor
        {
            set => connectedDoor = value;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}
