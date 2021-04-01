using System;
using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.HUD.HUDItems
{
    public class HealthHUDItem : IHUDItem
    {
        private Vector2 origin;
        private Vector2 center;
        private Texture2D uiSpriteTexture;
        private Texture2D heartTexture;
        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;
        private IPlayer player;

        private Vector2 heartContainerOrigin;

        private float heartCount;
        private float totalHeartCount;
        private List<HeartHUDSprite> hearts;

        public HealthHUDItem(Game1 game, Texture2D UITexture, Texture2D heartTexture, Vector2 origin)
        {
            this.origin = origin;
            center = new Vector2(origin.X / 2f, origin.Y / 2f);
            
	        uiSpriteTexture = UITexture;
            size = new Vector2(uiSpriteTexture.Bounds.Width, uiSpriteTexture.Bounds.Height);
            box = new Rectangle((int)size.X, (int)size.Y, (int)size.X, (int)size.Y);
            spriteBatch = game.SpriteBatch;
            
	        player = game.Player;

            heartCount = player.Health;
            totalHeartCount = player.TotalHealthCount / 2;

	        this.heartTexture = heartTexture;
            hearts = new List<HeartHUDSprite>();
            heartContainerOrigin = new Vector2(origin.X + HeartConstants.HeartHUDContainerOffsetX, origin.Y + HeartConstants.HeartHUDContainerOffsetY);
            InstantiateHearts();
        }

        public Vector2 Center 
	    { 
	        get => center; 
	        set => center = value; 
	    }

        public Texture2D Texture
        {
            get => uiSpriteTexture;
        }

        public ref Rectangle Box
        {
            get => ref box;
        }

        public void Draw()
        { 
            spriteBatch.Draw(uiSpriteTexture, origin, null, Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.None, 0);
            DrawHeartDisplay();
        }

        public void Erase()
        {
            uiSpriteTexture.Dispose();
	    }

        public int Update(GameTime gameTime)
        {
            UpdateHearts();
            return 0;
	    }

        private void InstantiateHearts()
        {
            for (int i = 0; i < heartCount; i++)
            {
                Vector2 origin = heartContainerOrigin;
                if (i > 7)
                {
                    origin.Y += 8;
                    origin.X += (i - 8) * 8f;
                }
                else
                {
                    origin.X += i * 8f;
                }
                hearts.Add(new HeartHUDSprite(spriteBatch, heartTexture, origin));
            }
        }

        private void UpdateHearts()
        { 
            if (heartCount != player.Health)
            {
                heartCount = player.Health;
            }
            if (totalHeartCount != player.TotalHealthCount)
            {
                totalHeartCount = player.TotalHealthCount / 2;
            }
        }

        private void DrawHeartDisplay()
        {
            int fullCount = (int)heartCount / 2;

            int i = 0;
	        for (; i < fullCount; i++)
            {
                hearts[i].Full = true;
                hearts[i].Draw();
            }

            if (heartCount % 2 > 0)
            {
                hearts[i].Half = true;
                hearts[i].Draw();
                i++;
            }

            for (; i < totalHeartCount; i++)
            {
                hearts[i].Empty = true;
                hearts[i].Draw();
            }
        }
    }
}
