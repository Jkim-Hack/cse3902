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
        public List<BlockSprite> blocks { get; set; }

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

        public void LoadNewRoom(ref List<BlockSprite> oldList, List<BlockSprite> newList)
        {
            oldList = new List<BlockSprite>();

            for (int i = 0; i < blocks.Count; i++)
            {
                oldList[i] = blocks[i];
            }

            blocks = new List<BlockSprite>();

            for (int i = 0; i < newList.Count; i++)
            {
                blocks[i] = newList[i];
            }

        }
    }
}
