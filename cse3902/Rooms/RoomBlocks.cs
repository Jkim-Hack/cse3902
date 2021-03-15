using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Rooms
{
    public class RoomBlocks
    {
        public List<IBlock> blocks { get; set; }

        private static RoomBlocks instance = new RoomBlocks();

        public static RoomBlocks Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomBlocks()
        {
            blocks = new List<IBlock>();
        }

        public void AddBlock(IBlock block)
        {
            blocks.Add(block);
        }

        public void RemoveBlock(IBlock block)
        {
            (blocks as List<IBlock>).RemoveAll(x => x.Center == block.Center);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IEntity enemy in blocks)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (IEntity enemy in blocks)
            {
                enemy.Draw();
            }
        }

        public void LoadNewRoom(ref List<IBlock> oldList, List<IBlock> newList)
        {
            oldList = new List<IBlock>();

            for (int i = 0; i < blocks.Count; i++)
            {
                oldList[i] = blocks[i];
            }

            blocks = new List<IBlock>();

            for (int i = 0; i < newList.Count; i++)
            {
                blocks[i] = newList[i];
            }

        }
    }
}
