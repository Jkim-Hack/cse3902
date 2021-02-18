using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Items
{
    public class ItemHandler
    {
        


        public ItemHandler()
        {
            items = new List<ISprite>();
            itemIndex = 0;
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {

        }
        {
        }

        public void Draw()
        {
            items[itemIndex].Draw();
        }

        public void displayNext()
        {
            itemIndex++;
            if (itemIndex >= items.Count)
            {
                itemIndex = 0;
            }
            items[itemIndex].Draw();
        }

        public void displayPrev()
        {
            itemIndex--;
            if (itemIndex < 0)
            {
                itemIndex = items.Count - 1;
            }
            items[itemIndex].Draw();
        }

    }
}
