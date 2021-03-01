using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;

namespace cse3902.Entities
{
    public class BlockHandler
    {
        private List<BlockSprite> blocks;
        private Game1 game;
        private int blockIndex;

        private int maxFrameTime;
        private int currentFrameTime;

        public BlockHandler(Game1 thegame)
        {
            blocks = new List<BlockSprite>();
            blockIndex = 0;
            game = thegame;

            maxFrameTime = 10;
            currentFrameTime = 0;
        }

        public void LoadContent()
        {
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("Block1"), new Vector2(120, 120)));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block2"), new Vector2(120, 120)));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block3"), new Vector2(120, 120)));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block4"), new Vector2(120, 120)));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block5"), new Vector2(120, 120)));

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
