using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class BlockSpriteFactory : ISpriteFactory
    {
        private Dictionary<string, Texture2D> blockTextures;

        private static BlockSpriteFactory blockSpriteFactoryInstance = new BlockSpriteFactory();

        public static BlockSpriteFactory Instance
        {
            get => blockSpriteFactoryInstance;
        }

        private BlockSpriteFactory()
        {
            blockTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            blockTextures.Add("block", content.Load<Texture2D>("Block1"));
            blockTextures.Add("waterblock", content.Load<Texture2D>("block3"));
        }

        public ISprite CreateNormalBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["block"], startingPos);
        }

        public ISprite CreateWaterBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["waterblock"], startingPos);
        }
    }
}
