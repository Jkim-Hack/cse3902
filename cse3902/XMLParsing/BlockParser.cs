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

        public BlockParser(Game1 gm)
        {
            game = gm;
        }

        public void parseBlocks(Room roomobj, XElement roomxml, XDocument doc)
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

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                IBlock blockAdd = createBlock(typeName.Value, dir.Value, new Vector2(x, y));
                roomobj.AddBlock(blockAdd);
            }

        }


        public IBlock createBlock(String type, String dir, Vector2 startingPos)
        {
            IBlock newBlock = null;
            switch (type)
            {
                case "Normal":
                    if (dir.Equals("Still"))
                    {
                        newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 10, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, startingPos));
                    }
                    else if (dir.Equals("Down"))
                    {
                        newBlock = new NormalBlock(game, IBlock.PushDirection.Down, 10, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, startingPos));
                    }
                    break;
                case "Water":
                    newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 10, BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.spriteBatch, startingPos));
                    break;
                default:
                    //createdItem = null;
                    break;
            }
            return newBlock;
        }

    }
}
