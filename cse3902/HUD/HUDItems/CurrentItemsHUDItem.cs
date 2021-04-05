using System;
using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.HUD.HUDItems
{
    public class CurrentItemsHUDItem : IHUDItem
    {
        private Vector2 origin;
        private Vector2 center;
        private Texture2D uiSpriteTexture;
        private Texture2D rupeeTexture;
        private Texture2D keyTexture;
        private Texture2D bombTexture;

        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;
        private IPlayer player;


        public CurrentItemsHUDItem(Game1 game, Texture2D UITexture, Texture2D rupeeTexture, Texture2D keyTexture, Texture2D bombTexture, Vector2 origin)
        {
            this.origin = origin;
            center = new Vector2(origin.X / 2f, origin.Y / 2f);

            uiSpriteTexture = UITexture;
            this.rupeeTexture = rupeeTexture;
            this.keyTexture = keyTexture;
            this.bombTexture = bombTexture;

            size = new Vector2(uiSpriteTexture.Bounds.Width, uiSpriteTexture.Bounds.Height);
            box = new Rectangle((int)size.X, (int)size.Y, (int)size.X, (int)size.Y);
            spriteBatch = game.SpriteBatch;

            player = game.Player;
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
            spriteBatch.Draw(uiSpriteTexture, origin, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.HealthHUDLayer);

        }

        public void Erase()
        {
            uiSpriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }
    }
}
