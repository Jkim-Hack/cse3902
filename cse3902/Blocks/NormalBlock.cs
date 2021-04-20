using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;

namespace cse3902.Blocks
{
    class NormalBlock : IBlock
    {
        private readonly Game1 game;

        private ISprite normalBlockSprite;

        private Vector2 initialPosition;
        private float pixelsToPush;
        private const int pushThreshold = MovementConstants.BlockPushThreshold;

        private Vector2 pushingDirection;

        private float remainingPixelsToPush;
        private int remainingPush;
        private const float pushSpeed = MovementConstants.BlockPushSpeed;

        private bool justPushed;
        private bool isMoving;

        private Vector2 BlockPushingVector(IBlock.PushDirection direction)
        {
            switch (direction)
            {
                case IBlock.PushDirection.Up:
                    return new Vector2(0, -1);
                case IBlock.PushDirection.Down:
                    return new Vector2(0, 1);
                case IBlock.PushDirection.Left:
                    return new Vector2(-1, 0);
                case IBlock.PushDirection.Right:
                    return new Vector2(1, 0);
                case IBlock.PushDirection.Still:
                    return new Vector2(0, 0);
                default: //this should never happen
                    return new Vector2(0, 0);
            }
        }

        private ICollidable collidable;

        public NormalBlock(Game1 game, int pixelsToPush, ISprite sprite, Vector2 center)
        {
            this.game = game;

            normalBlockSprite = sprite;
            remainingPixelsToPush = pixelsToPush;
            this.pixelsToPush = pixelsToPush;
            initialPosition = center;
            remainingPush = pushThreshold;
            justPushed = false;
            isMoving = false;
            pushingDirection = new Vector2();

            this.collidable = new BlockCollidable(this);
        }

        public void Interact(IBlock.PushDirection pushDirection)
        {
            Interact(BlockPushingVector(pushDirection));
        }
        public void Interact(Vector2 pushDirection)
        {
            if (!isMoving && remainingPixelsToPush > 0)
            {
                justPushed = true;
                remainingPush--;
                pushingDirection = pushDirection;
                if (remainingPush == 0) isMoving = true;
            }
        }
        public void Update()
        {
            if (!isMoving)
            {
                if (!justPushed) remainingPush = pushThreshold;
            } 
            else
            {
                normalBlockSprite.Center += pushingDirection * pushSpeed;
                remainingPixelsToPush -= pushSpeed;

                //block was pushed a little too far and needs to be partially undone
                if (remainingPixelsToPush < 0)
                {
                    normalBlockSprite.Center += remainingPixelsToPush * pushingDirection;
                    isMoving = false;
                }
            }

            justPushed = false;
        }
        public void Draw()
        {
            normalBlockSprite.Draw();
        }
        public bool IsMoved()
        {
            return !isMoving && remainingPixelsToPush <= 0;
        }
        public void Reset()
        {
            normalBlockSprite.Center = initialPosition;
            remainingPixelsToPush = pixelsToPush;
            remainingPush = pushThreshold;

            isMoving = false;
            justPushed = false;
        }

        public ref Rectangle Bounds
        {
            get => ref normalBlockSprite.Box;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public Vector2 Center
        {
            get => this.normalBlockSprite.Center;
        }
    }
}
