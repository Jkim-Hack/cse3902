using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomBlocks
    {
        public IList blocks;

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

            List<IBlock> blocksCast = blocks as List<IBlock>;

            for (int i = 0; i < blocksCast.Count; i++)
            {
                oldList[i] = blocksCast[i];
            }

            blocks = new List<IBlock>();

            for (int i = 0; i < newList.Count; i++)
            {
                blocks[i] = newList[i];
            }

        }

        public ref IList ListRef
        {
            get => ref blocks;
        }
    }
}