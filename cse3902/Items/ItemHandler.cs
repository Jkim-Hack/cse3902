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
        
        private List<ISprite> items;
        private int itemIndex;

        private int maxframetime;
        private int currentframetime;

        public ItemHandler()
        {
            items = new List<ISprite>();
            itemIndex = 0;
            maxframetime = 10;
            currentframetime = 0;
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            ItemSpriteFactory.Instance.LoadAllTextures(content);

            items.Add(ItemSpriteFactory.Instance.CreateArrowItem(spriteBatch, new Vector2(100, 100), new Vector2(0,-1)));
            items.Add(ItemSpriteFactory.Instance.CreateBombItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateBoomerangItem(spriteBatch, new Vector2(100, 100), new Vector2(-1, 0)));
            items.Add(ItemSpriteFactory.Instance.CreateBowItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateClockItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateCompassItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateFairyItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateHeartContainerItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateHeartItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateKeyItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateMapItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateTriforceItem(spriteBatch, new Vector2(100, 100)));
            items.Add(ItemSpriteFactory.Instance.CreateSwordItem(spriteBatch, new Vector2(100, 100), new Vector2(-1, 0)));
        }

        public void Update()
        {
            if (currentframetime < maxframetime) currentframetime++;
        }

        public void Draw()
        {
            items[itemIndex].Draw();
        }

        public void displayNext()
        {
            if (currentframetime == maxframetime)
            {
                itemIndex++;
                if (itemIndex >= items.Count)
                {
                    itemIndex = 0;
                }
                currentframetime = 0;
            }
        }

        public void displayPrev()
        {
            if (currentframetime == maxframetime)
            {
                itemIndex--;
                if (itemIndex < 0)
                {
                    itemIndex = items.Count - 1;
                }
                currentframetime = 0;
            }
        }
        public void Reset()
        {
            itemIndex = 0;
        }

    }
}
