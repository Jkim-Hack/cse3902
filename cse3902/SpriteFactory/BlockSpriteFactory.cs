using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Rooms;
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
            blockTextures.Add("normalblock", content.Load<Texture2D>("normalblock"));
            DungeonMask.Instance.addTexture(blockTextures["normalblock"]);
            blockTextures.Add("waterblock", content.Load<Texture2D>("newwaterblock"));
            DungeonMask.Instance.addTexture(blockTextures["waterblock"]);
            blockTextures.Add("brick", content.Load<Texture2D>("brick"));
            DungeonMask.Instance.addTexture(blockTextures["brick"]);
            blockTextures.Add("ladder", content.Load<Texture2D>("ladder"));
            DungeonMask.Instance.addTexture(blockTextures["ladder"]);
            blockTextures.Add("invis", content.Load<Texture2D>("invisibleblock"));
            //blockTextures.Add("normalgrey", content.Load<Texture2D>("normalgrey"));
            //blockTextures.Add("normalred", content.Load<Texture2D>("normalred"));
            //blockTextures.Add("normalgreen", content.Load<Texture2D>("normalgreen"));
            //blockTextures.Add("redwater", content.Load<Texture2D>("redwater"));
            //blockTextures.Add("lava", content.Load<Texture2D>("lava"));
        }

        public ISprite CreateNormalBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }

        public ISprite CreateWaterBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["waterblock"], startingPos);
        }

        public ISprite CreateBrickSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["brick"], startingPos);
        }

        public ISprite CreateLadderSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["ladder"], startingPos);
        }

        public ISprite CreateInvisibleBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["invis"], startingPos);
        }

        //public ISprite CreateNormalGreyBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        //{
        //    return new BlockSprite(spriteBatch, blockTextures["normalgrey"], startingPos);
        //}

        //public ISprite CreateNormalRedBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        //{
        //    return new BlockSprite(spriteBatch, blockTextures["normalred"], startingPos);
        //}

        //public ISprite CreateNormalGreenBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        //{
        //    return new BlockSprite(spriteBatch, blockTextures["normalgreen"], startingPos);
        //}

        //public ISprite CreateRedWaterBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        //{
        //    return new BlockSprite(spriteBatch, blockTextures["redwater"], startingPos);
        //}

        //public ISprite CreateLavaBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        //{
        //    return new BlockSprite(spriteBatch, blockTextures["lava"], startingPos);
        //}

        //TODO: once the textures are loaded in, delete the below code and uncomment the above code
        
        public ISprite CreateNormalGreyBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }

        public ISprite CreateNormalRedBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }

        public ISprite CreateNormalGreenBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }

        public ISprite CreateRedWaterBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }

        public ISprite CreateLavaBlockSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BlockSprite(spriteBatch, blockTextures["normalblock"], startingPos);
        }
    }
}
