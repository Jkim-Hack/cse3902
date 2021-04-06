using System.Collections.Generic;
using cse3902.Constants;
using cse3902.SpriteFactory;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using cse3902.HUD.HUDItems;

namespace cse3902.HUD
{
    public class HUDManager
    {
        public enum HUDItemKey
        {
            INVENTORY,
            MAP_COMPASS_ITEM,
            ORANGE_MAP,
            MINIMAP,
            HEALTH
        }

        private Game1 game;
        private Dictionary<HUDItemKey, IHUDItem> HUDItems;

        private const int backgroundOffsetX = 40;
        private const int backgroundOffsetY = 40;

        public HUDManager(Game1 game)
        {
            this.game = game;
            HUDItems = new Dictionary<HUDItemKey, IHUDItem>();
        }

        private void DrawBlackBackground()
        {
            HUDUtilities.DrawRectangle(game, new Rectangle(0 - backgroundOffsetX, 0 - backgroundOffsetY, DimensionConstants.OriginalWindowWidth, DimensionConstants.OriginalWindowHeight), Color.Black, backgroundOffsetX, backgroundOffsetY, HUDUtilities.BackgroundLayer);
        }

        // This will only be called once per HUD item
        public void CreateHUDItemWithKey(HUDItemKey key)
        {
            switch (key)
            {
                case HUDItemKey.INVENTORY:
                    HUDItems.Add(key, CreateInventoryHUDItem());
                    break;
                case HUDItemKey.HEALTH:
                    HUDItems.Add(key, CreateHealthHUDItem());
                    break;
                case HUDItemKey.MINIMAP:
                    HUDItems.Add(key, CreateMinimapHUDItem());
                    break;
                case HUDItemKey.MAP_COMPASS_ITEM:
                    HUDItems.Add(key, CreateMapCompassHUDItem());
                    break;
                case HUDItemKey.ORANGE_MAP:
                    HUDItems.Add(key, CreateOrangeMapHUDItem());
                    break;
                // ... Add more when new HUDItems are implemented
            }
        }

        private IHUDItem CreateInventoryHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateInventoryHUDItem(game, HUDPositionConstants.InventoryHUDPosition);
        }
        
	    private IHUDItem CreateHealthHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateHealthHUDItem(game, HUDPositionConstants.HealthHUDPosition);
        }

        private IHUDItem CreateMinimapHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateMinimapHUDItem(game);
        }

        private IHUDItem CreateMapCompassHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateMapCompassHUDItem(game);
        }

        private IHUDItem CreateOrangeMapHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateOrangeMapHUDItem(game);
        }
 
	    public void Update(GameTime gameTime)
        {
            foreach (var hudItem in HUDItems.Values)
            {
                hudItem.Update(gameTime);
            }
        }

        public void Draw()
        {
            DrawBlackBackground();
            foreach (var hudItem in HUDItems.Values)
            {
                hudItem.Draw();
            }
        }

        public InventoryHUDItem HudInventory
        {
            get => HUDItems[HUDItemKey.INVENTORY] as InventoryHUDItem;
        }
    }
}
