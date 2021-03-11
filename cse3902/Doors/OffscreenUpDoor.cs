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

        public OffscreenUpDoor(Game1 game, Vector2 center)
        {
            this.game = game;
            roomTranslationVector = new Vector3(0, 0, 1);
            centerPosition = center;
        }

        public void Interact()
        {
            game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector);
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
    }
}
