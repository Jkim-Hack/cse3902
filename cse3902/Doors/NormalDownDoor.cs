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
        private IDoor connectedDoor;

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
                    game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector, connectedDoor);
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
        public Vector2 PlayerReleasePosition()
        {
            return doorSprite.Center + new Vector2(0, 16);
        }
        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, -1);
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
        public IDoor ConnectedDoor
        {
            set => connectedDoor = value;
        }
    }
}
