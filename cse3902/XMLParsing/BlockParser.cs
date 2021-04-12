using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Blocks;
using cse3902.SpriteFactory;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class BlockParser
    {
        private Game1 game;

        private const int BLOCK_SIDE = RoomUtilities.BLOCK_SIDE;

        public BlockParser(Game1 gm)
        {
            game = gm;
        }

        public void ParseBlocks(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName blocksName = XName.Get("blocks", doc.Root.Name.NamespaceName);

            XElement blocks = roomxml.Element(blocksName);
            List<XElement> blockList = blocks.Elements("block").ToList();

            foreach (XElement block in blockList)
            {
                XElement typeName = block.Element("type");
                XElement xloc = block.Element("xloc");
                XElement yloc = block.Element("yloc");
                XElement push = block.Element("push");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);
                int numBlocks = Int32.Parse(push.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                IBlock blockAdd = CreateBlock(typeName.Value, truePos, numBlocks);
                if (blockAdd != null)
                {
                    roomobj.AddBlock(blockAdd);
                }
            }
        }

        private IBlock CreateBlock(String type, Vector2 startingPos, int numBlocks)
        {
            IBlock newBlock = new NormalBlock(game, BLOCK_SIDE * numBlocks, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
            switch (type)
            {
                case "Normal":
                    break;
                case "Water":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Ladder":
                    newBlock = new WalkableBlock(game, BlockSpriteFactory.Instance.CreateLadderSprite(game.SpriteBatch, startingPos));
                    break;
                case "Brick":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateBrickSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Invisible":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateInvisibleBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Red":
                    newBlock = new NormalBlock(game, 0,  BlockSpriteFactory.Instance.CreateRedWaterBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "NormalGrey":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateNormalGreyBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "NormalRed":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateNormalRedBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "NormalGreen":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateNormalGreenBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Lava":
                    newBlock = new NormalBlock(game, 0, BlockSpriteFactory.Instance.CreateLavaBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Wall":
                    Background.Instance.generateRoomWall(startingPos);
                    newBlock = null;
                    break;
                default:
                    break;
            }
            return newBlock;
        }

    }
}
