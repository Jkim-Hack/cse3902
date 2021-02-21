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


        public BlockHandler(Game1 thegame)
        {
            blocks = new List<BlockSprite>();
            blockIndex = 0;
            game = thegame;

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
            //blocks dont update
        }

        public void Draw()
        {
            blocks[blockIndex].Draw();
        }

        public void cycleNext()
        {
            
            blockIndex++;
            if (blockIndex > blocks.Count-1)
            {
                blockIndex = 0;
            }
             
        }

        public void cyclePrev()
        {
            blockIndex--;
            if (blockIndex < 0)
            {
                blockIndex = blocks.Count - 1;
            }
        }

        public void Reset()
        {
            blockIndex = 0;
        }
    }
}
