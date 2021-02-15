using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class ItemSpriteFactory : ISpriteFactory
    {
        private Dictionary<string, Texture2D> itemTextures;

	    private static ItemSpriteFactory itemSpriteFactoryInstance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get => itemSpriteFactoryInstance;
        }

        private ItemSpriteFactory()
        {
            itemTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            itemTextures.Add("arrow", content.Load<Texture2D>("arrow"));
            itemTextures.Add("bomb", content.Load<Texture2D>("bomb"));
            itemTextures.Add("boomerang", content.Load<Texture2D>("boomerang"));
            itemTextures.Add("bow", content.Load<Texture2D>("bow"));
            itemTextures.Add("clock", content.Load<Texture2D>("clock"));
            itemTextures.Add("compass", content.Load<Texture2D>("compass"));
            itemTextures.Add("fairy", content.Load<Texture2D>("fairy"));
            itemTextures.Add("heart", content.Load<Texture2D>("heart"));
            itemTextures.Add("heartcont", content.Load<Texture2D>("heartcont"));
            itemTextures.Add("key", content.Load<Texture2D>("key"));
            itemTextures.Add("map", content.Load<Texture2D>("map"));
            itemTextures.Add("triforce", content.Load<Texture2D>("triforce"));
        }

        public ISprite CreateArrowItemSprite(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 direction)
        {
            return new ArrowItem(spriteBatch, itemTextures["arrow"], startingPos, direction);
        }
	    
	    public ISprite CreateBombItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BombItem(spriteBatch, itemTextures["bomb"], startingPos);
        }
        
	    public ISprite CreateBoomerangItemSprite(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 direction)
        {
            return new BoomerangItem(spriteBatch, itemTextures["boomerang"], startingPos, direction);
        }
        
	    public ISprite CreateBowItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BowItem(spriteBatch, itemTextures["bow"], startingPos);
        }
	    
	    public ISprite CreateClockItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new ClockItem(spriteBatch, itemTextures["clock"], startingPos);
        }
	    
	    public ISprite CreateCompassItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new CompassItem(spriteBatch, itemTextures["compass"], startingPos);
        }
	    
	    public ISprite CreateFairyItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new FairyItem(spriteBatch, itemTextures["fairy"], startingPos);
        }
	    
	    public ISprite CreateHeartContainerItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new HeartContainerItem(spriteBatch, itemTextures["heartcont"], startingPos);
        }
	    
	    public ISprite CreateHeartItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new HeartItem(spriteBatch, itemTextures["heart"], startingPos);
        }
	    
	    public ISprite CreateKeyItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new KeyItem(spriteBatch, itemTextures["key"], startingPos);
        }
	    
	    public ISprite CreateMapItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new MapItem(spriteBatch, itemTextures["map"], startingPos);
        }
	    
	    public ISprite CreateTriforceItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new TriforceItem(spriteBatch, itemTextures["triforce"], startingPos);
        }
    }
}
