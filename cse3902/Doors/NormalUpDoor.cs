﻿using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.HUD;
using cse3902.Sounds;
using cse3902.Rooms;
using cse3902.Constants;

namespace cse3902.Doors
{
    public class NormalUpDoor : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;
        private IDoor.DoorState initialDoorState;
        private IDoor.DoorState doorState;
        private IDoor connectedDoor;
        private ICollidable collidable;

        public NormalUpDoor(Game1 game, Vector2 center, IDoor.DoorState initialDoorState)
        {
            this.game = game;
            this.initialDoorState = initialDoorState;
            doorSprite = DoorSpriteFactory.Instance.CreateUpDoorSprite(game.SpriteBatch, center, initialDoorState);
            roomTranslationVector = new Vector3(0, -1, 0);
            State = initialDoorState;

            this.collidable = new DoorCollidable(this);
        }

        public void Interact()
        {
            switch (doorState)
            {
                case IDoor.DoorState.Open:
                    connectedDoor.State = IDoor.DoorState.Open;
                    game.RoomHandler.LoadNewRoom(game.RoomHandler.currentRoom + roomTranslationVector, connectedDoor);
                    break;
                case IDoor.DoorState.Bombed:
                    game.RoomHandler.LoadNewRoom(game.RoomHandler.currentRoom + roomTranslationVector, connectedDoor);
                    break;
                case IDoor.DoorState.Closed:
                    break;
                case IDoor.DoorState.Locked:
                    if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Key] > 0)
                    {
                        InventoryManager.Instance.inventory[InventoryManager.ItemType.Key]--;
                        State = IDoor.DoorState.Open;
                        connectedDoor.State = IDoor.DoorState.Open;
                        SoundFactory.PlaySound(SoundFactory.Instance.doorUnlock);
                    }
                    break;
                case IDoor.DoorState.Wall: //do nothing
                    break;
                default: //this should never happen
                    break;
            }
        }

        public Vector2 PlayerReleasePosition()
        {
            return doorSprite.Center + new Vector2(0, -RoomUtilities.BLOCK_SIDE);
        }

        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, MovementConstants.NormalDoorRelease);
        }

        public void Draw()
        {
            doorSprite.Draw();
        }

        public void Reset()
        {
            if (initialDoorState == IDoor.DoorState.Closed) State = IDoor.DoorState.Closed;
        }

        public IDoor.DoorState State
        {
            get
            {
                return doorState;
            }
            set
            {
                doorState = value;
                doorSprite = DoorSpriteFactory.Instance.CreateUpDoorSprite(game.SpriteBatch, doorSprite.Center, value);
            }
        }

        public ref Rectangle Bounds
        {
            get
            {
                return ref doorSprite.Box;
            }
        }

        public IDoor ConnectedDoor
        {
            set => connectedDoor = value;
            get => connectedDoor;
        }

        public IDoorSprite DoorSprite
        {
            get => doorSprite;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}
