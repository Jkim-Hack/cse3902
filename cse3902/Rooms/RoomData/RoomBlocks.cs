using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomBlocks
    {
        public IList blocks;
        private IList oldBlocks;

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
            //blocks.Remove(block);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IBlock block in blocks)
            {
                block.Update();
            }
        }

        public void Draw()
        {
            foreach (IBlock block in blocks)
            {
                block.Draw();
            }
        }

        public void DrawOld()
        {
            foreach (IBlock block in oldBlocks)
            {
                block.Draw();
            }
        }

        public void LoadNewRoom(ref List<IBlock> oldList, List<IBlock> newList)
        {
            oldList = new List<IBlock>();
            oldBlocks = oldList;

            List<IBlock> blocksCast = blocks as List<IBlock>;

            for (int i = 0; i < blocks.Count; i++)
            {
                oldList.Add(blocksCast[i]);
            }

            blocks.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                blocks.Add(newList[i]);
            }

        }

        public void Reset()
        {
            foreach (IBlock block in blocks)
            {
                block.Reset();
            }
        }

        public ref IList ListRef
        {
            get => ref blocks;
        }
    }
}