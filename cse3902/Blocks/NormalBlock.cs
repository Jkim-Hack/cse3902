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
        int remainingPixelsToPush;
        private const int pushSpeed = 2;

        public NormalBlock(Game1 game, Vector2 center, IBlock.PushDirection direction, int pixelsToPush)
        {
            this.game = game;

            switch (direction)
            {
                case IBlock.PushDirection.Up:
                    blockPushingDirection = new Vector2(0, -1);
                    break;
                case IBlock.PushDirection.Down:
                    blockPushingDirection = new Vector2(0, 1);
                    break;
                case IBlock.PushDirection.Left:
                    blockPushingDirection = new Vector2(-1, 0);
                    break;
                case IBlock.PushDirection.Right:
                    blockPushingDirection = new Vector2(1, 0);
                    break;
                case IBlock.PushDirection.Still:
                    blockPushingDirection = new Vector2(0, 0);
                    break;
                default: //this should never happen
                    blockPushingDirection = new Vector2(0, 0);
                    break;
            }

            normalBlockSprite = BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, center);
            remainingPixelsToPush = pixelsToPush;
        }

        public void Move()
        {
            if (remainingPixelsToPush > 0)
            {
                normalBlockSprite.Center += blockPushingDirection * pushSpeed;
                remainingPixelsToPush -= pushSpeed;

                //block was pushed a little too far and needs to be partially undone
                if (remainingPixelsToPush < 0) normalBlockSprite.Center += remainingPixelsToPush * blockPushingDirection;
            }
        }
        public void Draw()
        {
            normalBlockSprite.Draw();
        }
    }
}
