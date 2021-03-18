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
                XElement dir = block.Element("dir");
                XElement xloc = block.Element("xloc");
                XElement yloc = block.Element("yloc");
                XElement push = block.Element("push");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);
                int numBlocks = Int32.Parse(push.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                IBlock blockAdd = CreateBlock(typeName.Value, dir.Value, truePos, numBlocks);
                roomobj.AddBlock(blockAdd);
            }
        }

        private IBlock CreateBlock(String type, String dir, Vector2 startingPos, int numBlocks)
        {
            IBlock newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 0, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
            switch (type)
            {
                case "Normal":
                    switch (dir)
                    {
                        case "Still":
                            newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 0, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
                            break;
                        case "Down":
                            newBlock = new NormalBlock(game, IBlock.PushDirection.Down, BLOCK_SIDE * numBlocks, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
                            break;
                        case "Up":
                            newBlock = new NormalBlock(game, IBlock.PushDirection.Up, BLOCK_SIDE * numBlocks, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
                            break;
                        case "Left":
                            newBlock = new NormalBlock(game, IBlock.PushDirection.Left, BLOCK_SIDE * numBlocks, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
                            break;
                        case "Right":
                            newBlock = new NormalBlock(game, IBlock.PushDirection.Right, BLOCK_SIDE * numBlocks, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.SpriteBatch, startingPos), startingPos);
                            break;
                        default: //this should never happen
                            break;
                    }
                    break;
                case "Water":
                    newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 0, BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Ladder":
                    newBlock = new WalkableBlock(game, BlockSpriteFactory.Instance.CreateLadderSprite(game.SpriteBatch, startingPos));
                    break;
                case "Brick":
                    newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 0, BlockSpriteFactory.Instance.CreateBrickSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                case "Movement0":
                    newBlock = new MovementBlock(game, startingPos, 0);
                    break;
                case "Movement1":
                    newBlock = new MovementBlock(game, startingPos, 1);
                    break;
                case "Invisible":
                    newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 0, BlockSpriteFactory.Instance.CreateInvisibleBlockSprite(game.SpriteBatch, startingPos), startingPos);
                    break;
                default:
                    break;
            }
            return newBlock;
        }

    }
}
