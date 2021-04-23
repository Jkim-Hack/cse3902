using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.Utilities;
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
            spriteBatch.Draw(uiSpriteTexture, origin, null, Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.None, HUDUtilities.HealthHUDLayer);
            DrawHeartDisplay();
        }

        public int Update(GameTime gameTime)
        {
            UpdateHearts();
            return 0;
	    }

        private void InstantiateHearts()
        {
            for (int i = 0; i < HeartConstants.MaxHeartCount / 2; i++)
            {
                Vector2 origin = heartContainerOrigin;
                if (i > HeartConstants.HeartDim - 1)
                {
                    origin.Y += HeartConstants.HeartDim;
                    origin.X += (i - HeartConstants.HeartDim) * (float)HeartConstants.HeartDim;
                }
                else
                {
                    origin.X += i * (float)HeartConstants.HeartDim;
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

            if (player.TotalHealthCount == 2 && player.Health > 0) return;

            for (; i < totalHeartCount; i++)
            {
                hearts[i].Empty = true;
                hearts[i].Draw();
                if (player.TotalHealthCount == 2) break;
            }
        }
    }
}
