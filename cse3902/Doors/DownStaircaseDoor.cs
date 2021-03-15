using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Doors
{
    public class DownStaircaseDoor : IDoor
    {
        private Game1 game;
        private IDoorSprite doorSprite;
        private Vector3 roomTranslationVector;
        private IDoor connectedDoor;
        private ICollidable collidable;

        public DownStaircaseDoor(Game1 game, Vector2 center)
        {
            this.game = game;
            doorSprite = DoorSpriteFactory.Instance.CreateStaircaseSprite(game.spriteBatch, center);
            roomTranslationVector = new Vector3(0, 0, -1);

            this.collidable = new DoorCollidable(this);
        }

        public void Interact()
        {
            game.roomHandler.LoadNewRoom(game.roomHandler.currentRoom + roomTranslationVector, connectedDoor);
        }
        public Vector2 PlayerReleasePosition()
        {
            return doorSprite.Center + new Vector2(-32, 31);
        }
        public Vector2 PlayerReleaseDirection()
        {
            return new Vector2(0, 1);
        }
        public void Draw()
        {
            doorSprite.Draw();
        }
        public void ChangeState(IDoor.DoorState doorState)
        {
            //do nothing
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
