using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using System.Collections.Generic;

namespace cse3902.Doors
{
    public class NormalDownDoor : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;
        private IDoor.DoorState doorState;

        public NormalDownDoor(Game1 game, Vector2 center, IDoor.DoorState initialDoorState)
        {
            this.game = game;
            doorSprite = DoorSpriteFactory.Instance.CreateDownDoorSprite(game.spriteBatch, center, initialDoorState);
            roomTranslationVector = new Vector3(0, 1, 0);
            doorState = initialDoorState;
        }

        public void Interact()
        {
            switch (doorState)
            {
                case IDoor.DoorState.Open:
                    game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector);
                    break;
                case IDoor.DoorState.Closed:
                case IDoor.DoorState.Locked:
                    doorState = IDoor.DoorState.Open;
                    doorSprite = DoorSpriteFactory.Instance.CreateDownDoorSprite(game.spriteBatch, doorSprite.Center, doorState);
                    break;/*
                case IDoor.DoorState.Locked:
                    if (game.player.inventory.contains(key))
                    {
                        game.player.inventory.key--;
                        doorState = IDoor.DoorState.Open;
                        doorSprite = DoorSpriteFactory.Instance.CreateDownDoorSprite(game.spriteBatch, doorSprite.Center, doorState);
                    }
                    break;*/
                case IDoor.DoorState.Wall: //do nothing
                    break;
                default: //this should never happen
                    break;
            }
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
