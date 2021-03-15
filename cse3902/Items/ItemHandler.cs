using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Items
{
    public class ItemHandler
    {
        private List<IItem> items;
        private int itemIndex;

        private int maxFrameTime;
        private int currentFrameTime;

        private SpriteBatch spriteBatch;

        public ItemHandler()
        {
            items = new List<IItem>();
            itemIndex = 0;
            maxFrameTime = 10;
            currentFrameTime = 0;
        }

        private void InitializeItems()
        {
            //items.Add(ItemSpriteFactory.Instance.CreateArrowItem(spriteBatch, new Vector2(100, 100), new Vector2(0, -1)));
            //items.Add(ItemSpriteFactory.Instance.CreateBombItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateBoomerangItem(spriteBatch, new Vector2(100, 100), new Vector2(-1, 0)));
            //items.Add(ItemSpriteFactory.Instance.CreateBowItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateClockItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateCompassItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateFairyItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateHeartContainerItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateHeartItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateKeyItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateMapItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateTriforceItem(spriteBatch, new Vector2(100, 100)));
            //items.Add(ItemSpriteFactory.Instance.CreateSwordProjectile(spriteBatch, new Vector2(100, 100), new Vector2(-1, 0)));
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            ItemSpriteFactory.Instance.LoadAllTextures(content);

            this.spriteBatch = spriteBatch;
            InitializeItems();
        }
       
        public void Update(GameTime gameTime)
        {
            foreach (var item in items)
            {
                item.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (var item in items)
            {
                item.Draw();
            }
        }

        public void CycleNext()
        {
            if (currentFrameTime == maxFrameTime)
            {
                itemIndex++;
                if (itemIndex >= items.Count)
                {
                    itemIndex = 0;
                }
                currentFrameTime = 0;
            }
        }

        public void SwitchOut(ref List<IItem> oldList, ref List<IItem> newList)
        {
            oldList = new List<IItem>();

            for (int i = 0; i < items.Count; i++)
            {
                oldList[i] = items[i];
            }

            items = new List<IItem>();

            for (int i = 0; i < newList.Count; i++)
            {
                items[i] = newList[i];
            }

        }

        public void CyclePrev()
        {
            if (currentFrameTime == maxFrameTime)
            {
                itemIndex--;
                if (itemIndex < 0)
                {
                    itemIndex = items.Count - 1;
                }
                currentFrameTime = 0;
            }
        }
        public void Reset()
        {
            items = new List<IItem>();
            InitializeItems();
            itemIndex = 0;
        }

    }
}
