using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Items
{
    public class ItemSpriteFactory
    {
        private Texture2D arrow;
        private Texture2D bomb;
        private Texture2D boomerang;
        private Texture2D bow;
        private Texture2D clock;
        private Texture2D fairy;
        private Texture2D compass;
        private Texture2D heart;
        private Texture2D heartcont;
        private Texture2D triforce;
        private Texture2D key;
        private Texture2D map;
        private Texture2D swordItems;
        private Texture2D swordWeapons;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            arrow = content.Load<Texture2D>("arrow");
            bomb = content.Load<Texture2D>("bombnew");
            boomerang = content.Load<Texture2D>("boomerang");
            bow = content.Load<Texture2D>("bow");
            clock = content.Load<Texture2D>("clock");
            fairy = content.Load<Texture2D>("fairy");
            compass = content.Load<Texture2D>("compass");
            heart = content.Load<Texture2D>("heart");
            heartcont = content.Load<Texture2D>("heartcont");
            triforce = content.Load<Texture2D>("triforce");
            key = content.Load<Texture2D>("key");
            map = content.Load<Texture2D>("map");
            swordItems = content.Load<Texture2D>("SwordItem");
            swordWeapons = content.Load<Texture2D>("SwordAnimation");
        }

        public ISprite CreateArrowItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            return new ArrowItem(spriteBatch, arrow, startingPos, dir);
        }

        public ISprite CreateBombItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BombItem(spriteBatch, bomb, startingPos);
        }

        public ISprite CreateBoomerangItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            return new BoomerangItem(spriteBatch, boomerang, startingPos, dir);
        }

        public ISprite CreateBowItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new BowItem(spriteBatch, bow, startingPos);
        }

        public ISprite CreateClockItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new ClockItem(spriteBatch, clock, startingPos);
        }

        public ISprite CreateCompassItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new CompassItem(spriteBatch, compass, startingPos);
        }

        public ISprite CreateFairyItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new FairyItem(spriteBatch, fairy, startingPos);
        }

        public ISprite CreateHeartContainerItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new HeartItem(spriteBatch, heart, startingPos);
        }

        public ISprite CreateHeartItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new HeartContainerItem(spriteBatch, heartcont, startingPos);
        }

        public ISprite CreateKeyItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new KeyItem(spriteBatch, key, startingPos);
        }

        public ISprite CreateMapItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new MapItem(spriteBatch, map, startingPos);
        }

        public ISprite CreateSwordItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            return new SwordItem(spriteBatch, swordItems, startingPos, dir);
        }

        public ISprite CreateSwordWeapon(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            return new SwordItem(spriteBatch, swordWeapons, startingPos, dir);
        }

        public ISprite CreateTriforceItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new TriforceItem(spriteBatch, triforce, startingPos);
        }

    }
}
