using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Blocks
{
    class NormalBlock : IBlock
    {
        private readonly Game1 game;

        private ISprite normalBlockSprite;

        private Vector2 blockPushingDirection;
        private int remainingPixelsToPush;
        private const int pushSpeed = 2;

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
        public NormalBlock(Game1 game, Vector2 center, IBlock.PushDirection direction, int pixelsToPush)
        {
            this.game = game;

            blockPushingDirection = BlockPushingVector(direction);

            normalBlockSprite = BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, center);
            remainingPixelsToPush = pixelsToPush;
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
    }
}
