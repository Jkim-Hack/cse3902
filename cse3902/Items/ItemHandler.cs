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
        
        public List<ISprite> items;
        public int itemIndex { get; set; }
        private int itemDuration = 0;


        public ItemHandler()
        {
            items = new List<ISprite>();
            itemIndex = 0;
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            items.Add(new ArrowItem(spriteBatch, content.Load<Texture2D>("arrow"), new Vector2(100, 100)));
            items.Add(new BombItem(spriteBatch, content.Load<Texture2D>("bombnew"), new Vector2(100, 100)));
            items.Add(new BoomerangItem(spriteBatch, content.Load<Texture2D>("boomerang"), new Vector2(100, 100)));
            items.Add(new BowItem(spriteBatch, content.Load<Texture2D>("bow"), new Vector2(100, 100)));
            items.Add(new ClockItem(spriteBatch, content.Load<Texture2D>("clock"), new Vector2(100, 100)));
            items.Add(new FairyItem(spriteBatch, content.Load<Texture2D>("fairy"), new Vector2(100, 100)));
            items.Add(new CompassItem(spriteBatch, content.Load<Texture2D>("compass"), new Vector2(100, 100)));
            items.Add(new HeartItem(spriteBatch, content.Load<Texture2D>("heart"), new Vector2(100, 100)));
            items.Add(new HeartContainerItem(spriteBatch, content.Load<Texture2D>("heartcont"), new Vector2(100, 100)));
            items.Add(new TriforceItem(spriteBatch, content.Load<Texture2D>("triforce"), new Vector2(100, 100)));
            items.Add(new KeyItem(spriteBatch, content.Load<Texture2D>("key"), new Vector2(100, 100)));
            items.Add(new MapItem(spriteBatch, content.Load<Texture2D>("map"), new Vector2(100, 100)));


            foreach (ISprite item in items)
            {
                item.StartingPosition = new Vector2(100, 100);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (itemDuration < 100)
            {
                items[itemIndex].Update(gameTime);
                itemDuration++;
            }
            else
            {
                itemDuration = 0;
                itemIndex++;
                if (itemIndex >= items.Count)
                {
                    itemIndex = 0;
                }
            }
        }

        public void Draw()
        {
            items[itemIndex].Draw();
        }


    }
}
