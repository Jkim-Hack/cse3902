﻿using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Doors
{
    public class PortalDown : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;
        private IDoor connectedPortal;
        private ICollidable collidable;

        public PortalDown(Game1 game, Vector2 center)
        {
            this.game = game;
            doorSprite = DoorSpriteFactory.Instance.CreatePortalSprite(game.SpriteBatch, center);
            roomTranslationVector = new Vector3(0, 0, -2);

            this.collidable = new DoorCollidable(this);
        }

        public void Interact()
        {
            game.RoomHandler.LoadNewRoom(game.RoomHandler.currentRoom + roomTranslationVector, connectedPortal);
        }
        public Vector2 PlayerReleasePosition()
        {
            return doorSprite.Center;
        }
        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, -20);
        }
        public void Draw()
        {
            doorSprite.Draw();
        }
        public void Reset()
        {
            //doesn't reset
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
                return ref doorSprite.Box;
            }
        }
        public IDoor ConnectedDoor
        {
            set => connectedPortal = value;
            get => connectedPortal;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}