using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.SpriteFactory;
using cse3902.Interfaces;

namespace cse3902.Entities
{
    public class BlockHandler
    {
        private List<ISprite> blocks;
        private Game1 game;
        private int blockIndex;

        private int maxFrameTime;
        private int currentFrameTime;

        public BlockHandler(Game1 thegame)
        {
            blocks = new List<ISprite>();
            blockIndex = 0;
            game = thegame;

            maxFrameTime = 10;
            currentFrameTime = 0;
        }

        public void LoadContent()
        {
            blocks.Add(BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, new Vector2(120, 120)));
            blocks.Add(BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.spriteBatch, new Vector2(120, 120)));
        }

        public void Update(GameTime gameTime)
        {
            if (currentFrameTime < maxFrameTime) currentFrameTime++;
        }

        public void Draw()
        {
            blocks[blockIndex].Draw();
        }

        public void CycleNext()
        {
            if (currentFrameTime == maxFrameTime)
            {
                blockIndex++;
                if (blockIndex == blocks.Count) blockIndex = 0;
                currentFrameTime = 0;
            }
        }

        public void CyclePrev()
        {
            if (currentFrameTime == maxFrameTime)
            {
                blockIndex--;
                if (blockIndex == -1) blockIndex = blocks.Count - 1;
                currentFrameTime = 0;
            }
        }

        public void Reset()
        {
            blockIndex = 0;
        }
    }
}
