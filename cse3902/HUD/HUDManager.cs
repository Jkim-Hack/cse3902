using System;
using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.HUD
{
    public class HUDManager
    {
        public enum HUDItemKey
        {
            INVENTORY,
            MAP_ITEM,
            COMPASS_ITEM,
            YELLOW_MAP,
            MINIMAP,
            COLLECTABLES,
            B_ITEM,
            A_ITEM,
            HEALTH
        }

        private Game1 game;
        private Dictionary<HUDItemKey, IHUDItem> HUDItems;

        public HUDManager(Game1 game)
        {
            this.game = game;
            HUDItems = new Dictionary<HUDItemKey, IHUDItem>();
        }

        // This will only be called once per HUD item
        public void CreateHUDItemWithKey(HUDItemKey key)
        {
            switch (key)
            {
                case HUDItemKey.INVENTORY:
                    break;
                case HUDItemKey.HEALTH:
                    CreateHealthHUDItem();
                    break;
                // ... Add more when new HUDItems are implemented
            }
        }

        private IHUDItem CreateHealthHUDItem()
        {
            return HUDSpriteFactory.Instance.CreateHealthHUDItem(game, HUDPositionConstants.HealthHUDPosition);
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
            foreach (var hudItem in HUDItems.Values)
            {
                hudItem.Draw();
            }
        }
    }
}
