using System;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class LinkSpriteFactory : ISpriteFactory
    {
        private Texture2D linkSpritesheet;
        private Vector2 defaultCenter;

        private static LinkSpriteFactory linkSpriteFactoryInstance = new LinkSpriteFactory();

        public static LinkSpriteFactory Instance
        {
            get => linkSpriteFactoryInstance;
        }

        private LinkSpriteFactory()
        {
            defaultCenter = new Vector2(200, 200);
        }

        public void LoadAllTextures(ContentManager content)
        {
            linkSpritesheet = content.Load<Texture2D>("Link");
        }

        public ISprite CreateLinkSprite(SpriteBatch spriteBatch)
        { 
            return new LinkSprite(spriteBatch, linkSpritesheet, 3, 3, defaultCenter);
        }

        public ISprite CreateLinkSprite(SpriteBatch spriteBatch, Vector2 centerPosition)
        { 
            return new LinkSprite(spriteBatch, linkSpritesheet, 3, 3, centerPosition);
        }
    }
}
