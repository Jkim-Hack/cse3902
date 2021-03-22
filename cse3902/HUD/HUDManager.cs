using System;
using System.Collections.Generic;
using cse3902.Interfaces;

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
    }
}
