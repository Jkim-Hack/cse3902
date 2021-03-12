using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using System.Collections.Generic;

namespace cse3902.Doors
{
    public class OffscreenUpDoor : IDoor
    {
        private Game1 game;
        private Vector3 roomTranslationVector;
        private Vector2 centerPosition;
        private IDoor connectedDoor;

        public OffscreenUpDoor(Game1 game, Vector2 center)
        {
            this.game = game;
            roomTranslationVector = new Vector3(0, 0, 1);
            centerPosition = center;
        }

        public void Interact()
        {
            game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector, connectedDoor);
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
        public List<Rectangle> Bounds
        {
            get
            {
                Rectangle destination = new Rectangle((int)centerPosition.X, (int)centerPosition.Y, 16, 16);
                destination.Offset(-destination.Width / 2, -destination.Height / 2);
                List<Rectangle> hitbox = new List<Rectangle>();
                hitbox.Add(destination);

                return hitbox;
            }
        }
        public IDoor ConnectedDoor
        {
            set => connectedDoor = value;
        }
    }
}
