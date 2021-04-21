using System;
using System.Collections.Generic;
using cse3902.HUD;
using cse3902.HUD.HUDItems;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class HUDSpriteFactory
    {
        private Dictionary<String, Texture2D> textures;
        private Texture2D [] labels;

        private static HUDSpriteFactory instance = new HUDSpriteFactory();

        public static HUDSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private HUDSpriteFactory()
        {
            textures = new Dictionary<string, Texture2D>();
            labels = new Texture2D[4];
        }

        public void LoadAllTextures(ContentManager content)
        {
            textures.Clear();
            textures.Add("InventoryTexture", content.Load<Texture2D>("UI/Inventory"));
            textures.Add("ItemsTexture", content.Load<Texture2D>("UI/HUDInventoryItems"));
            textures.Add("CurrentItemsTexture", content.Load<Texture2D>("UI/collectablesUI"));
            textures.Add("NumbersTexture", content.Load<Texture2D>("UI/collectablenumbers"));
            textures.Add("CursorTexture", content.Load<Texture2D>("UI/Cursor"));
            textures.Add("HealthUITexture", content.Load<Texture2D>("UI/HealthUI"));
            textures.Add("HeartUITexture", content.Load<Texture2D>("UI/HeartsUI"));
            textures.Add("MapCompassLabel", content.Load<Texture2D>("UI/map_compass_item"));
            textures.Add("Compass", content.Load<Texture2D>("compass"));
            textures.Add("OrangeMap", content.Load<Texture2D>("UI/orange_map"));
            textures.Add("OrangeMapRooms", content.Load<Texture2D>("UI/orange_map_rooms"));
            labels[0] = content.Load<Texture2D>("UI/level1");
            labels[1] = content.Load<Texture2D>("UI/level2");
            labels[2] = content.Load<Texture2D>("UI/level3");
            labels[3] = content.Load<Texture2D>("UI/level4");
        }

        public IHUDItem CreateInventoryHUDItem(Game1 game, Vector2 startingPos)
        {
            return new InventoryHUDItem(game, textures["InventoryTexture"], textures["CursorTexture"], startingPos);
        }
        public InventoryItemSprite CreateInventoryItemSprite(SpriteBatch spriteBatch, Vector2 center, InventoryManager.ItemType type)
        {
            return new InventoryItemSprite(spriteBatch, textures["ItemsTexture"], center, type);
        }

        public IHUDItem CreateCurrentItemsHUDItem(Game1 game, Vector2 startingPos)
        {
            return new CurrentItemsHUDItem(game, textures["CurrentItemsTexture"], textures["NumbersTexture"], startingPos);
        }

        public IHUDItem CreateHealthHUDItem(Game1 game, Vector2 startingPos)
        {
            return new HealthHUDItem(game, textures["HealthUITexture"], textures["HeartUITexture"], startingPos);
        }

        public IHUDItem CreateMinimapHUDItem(Game1 game)
        {
            return new MiniMapHUDItem(game, labels);
        }

        public IHUDItem CreateMapCompassHUDItem(Game1 game)
        {
            return new MapCompassHUDItem(game, textures["MapCompassLabel"], textures["Compass"]);
        }

        public IHUDItem CreateOrangeMapHUDItem(Game1 game)
        {
            return new OrangeMapHUDItem(game, textures["OrangeMap"], textures["OrangeMapRooms"]);
        }
    }
}
