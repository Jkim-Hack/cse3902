using System;
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
        private Texture2D HealthUITexture;
        private Texture2D HeartUITexture;

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
        }

        public void LoadAllTextures(ContentManager content)
        {
            HealthUITexture = content.Load<Texture2D>("UI/HealthUI");
            HealthUITexture = content.Load<Texture2D>("UI/HeartsUI");
        }

        public IHUDItem CreateHealthHUDItem(Game1 game, Vector2 startingPos)
        {
            return new HealthHUDItem(game, HealthUITexture, HeartUITexture, startingPos);
        }

        public IHUDItem CreateMinimapHUDItem(Game1 game)
        {
            return new MiniMapHUDItem(game);
        }
    }
}
