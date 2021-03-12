using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using System.Collections.Generic;

namespace cse3902.Doors
{
    public class DownStaircaseDoor : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;

        public DownStaircaseDoor(Game1 game, Vector2 center)
        {
            this.game = game;
            doorSprite = DoorSpriteFactory.Instance.CreateStaircaseSprite(game.spriteBatch, center);
            roomTranslationVector = new Vector3(0, 0, -1);
        }

        public void Interact()
        {
            game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector);
        }
        public void Draw()
        {
            doorSprite.Draw();
        }
        public List<Rectangle> Bounds
        {
            get
            {
                return doorSprite.Boxes;
            }
        }
    }
}
