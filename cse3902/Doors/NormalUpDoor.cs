﻿using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Doors
{
    public class NormalUpDoor : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;
        private IDoor.DoorState doorState;
        private IDoor connectedDoor;
        private ICollidable collidable;

        public NormalUpDoor(Game1 game, Vector2 center, IDoor.DoorState initialDoorState)
        {
            this.game = game;
            doorSprite = DoorSpriteFactory.Instance.CreateUpDoorSprite(game.spriteBatch, center, initialDoorState);
            roomTranslationVector = new Vector3(0, -1, 0);
            doorState = initialDoorState;

            this.collidable = new DoorCollidable(this);
        }

        public void Interact()
        {
            switch (doorState)
            {
                case IDoor.DoorState.Open:
                case IDoor.DoorState.Bombed:
                    game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector, connectedDoor);
                    break;
                case IDoor.DoorState.Closed:
                case IDoor.DoorState.Locked:
                    State = IDoor.DoorState.Open;
                    connectedDoor.State = IDoor.DoorState.Open;
                    break;/*
                case IDoor.DoorState.Locked:
                    if (game.player.inventory.contains(key))
                    {
                        game.player.inventory.key--;
                        State = IDoor.DoorState.Open;
                        connectedDoor.State = IDoor.DoorState.Open;
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
            return doorSprite.Center + new Vector2(0, -16);
        }
        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, 50);
        }
        public void Draw()
        {
            doorSprite.Draw();
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
                doorSprite = DoorSpriteFactory.Instance.CreateUpDoorSprite(game.spriteBatch, doorSprite.Center, value);
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
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}
