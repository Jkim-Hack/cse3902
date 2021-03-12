using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Blocks
{
    class NormalBlock : IBlock
    {
        private readonly Game1 game;

        private ISprite normalBlockSprite;

        private Vector2 blockPushingDirection;
        private float remainingPixelsToPush;
        private const float pushSpeed = 0.5f;

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

        public NormalBlock(Game1 game, IBlock.PushDirection direction, int pixelsToPush, ISprite sprite)
        {
            this.game = game;

            blockPushingDirection = BlockPushingVector(direction);

            normalBlockSprite = sprite;
            remainingPixelsToPush = pixelsToPush;

            this.collidable = new BlockCollidable(this);
        }

        public void Move(IBlock.PushDirection pushDirection)
        {
            Move(BlockPushingVector(pushDirection));
        }
        public void Move(Vector2 pushDirection)
        {
            if (blockPushingDirection.Equals(pushDirection))
            {
                normalBlockSprite.Center += blockPushingDirection * pushSpeed;
                remainingPixelsToPush -= pushSpeed;

                //block was pushed a little too far and needs to be partially undone
                if (remainingPixelsToPush <= 0)
                {
                    normalBlockSprite.Center += remainingPixelsToPush * blockPushingDirection;
                    blockPushingDirection = new Vector2(0, 0);
                }
            }
        }
        public void Draw()
        {
            normalBlockSprite.Draw();
        }

        public ref Rectangle Bounds
        {
            get => ref normalBlockSprite.Box;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}
