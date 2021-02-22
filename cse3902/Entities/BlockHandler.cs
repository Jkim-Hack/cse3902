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

        private int maxframetime;
        private int currentframetime;

        public BlockHandler(Game1 thegame)
        {
            blocks = new List<BlockSprite>();
            blockIndex = 0;
            game = thegame;

            maxframetime = 10;
            currentframetime = 0;
        }

        public void LoadContent()
        {
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("Block1")));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block2")));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block3")));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block4")));
            blocks.Add(new BlockSprite(game.spriteBatch, game.Content.Load<Texture2D>("block5")));

        }

        public void Update(GameTime gameTime)
        {
            if (currentframetime < maxframetime) currentframetime++;
        }

        public void Draw()
        {
            blocks[blockIndex].Draw();
        }

        public void cycleNext()
        {
            if (currentframetime == maxframetime)
            {
                blockIndex++;
                if (blockIndex == blocks.Count) blockIndex = 0;
                currentframetime = 0;
            }
        }

        public void cyclePrev()
        {
            if (currentframetime == maxframetime)
            {
                blockIndex--;
                if (blockIndex == -1) blockIndex = blocks.Count - 1;
                currentframetime = 0;
            }
        }

        public void Reset()
        {
            blockIndex = 0;
        }
    }
}
