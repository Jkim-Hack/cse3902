﻿using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using cse3902.Items;
using cse3902.Rooms;
using System;

namespace cse3902.SpriteFactory
{
    public class ItemSpriteFactory
    {
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
        private Texture2D rupee;
        private Texture2D cloud;

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
            bomb = content.Load<Texture2D>("bomb");
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
            rupee = content.Load<Texture2D>("rupee");
            cloud = content.Load<Texture2D>("cloud");
        }

        public ISprite CreateBombItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new BombItem(spriteBatch, bomb, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateBoomerangItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new BoomerangItem(spriteBatch, boomerang, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateBowItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new BowItem(spriteBatch, bow, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateClockItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new ClockItem(spriteBatch, clock, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateCompassItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new CompassItem(spriteBatch, compass, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateFairyItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new FairyItem(spriteBatch, fairy, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateHeartContainerItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new HeartContainerItem(spriteBatch, heartcont, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateHeartItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new HeartItem(spriteBatch, heart, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateKeyItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new KeyItem(spriteBatch, key, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateMapItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new MapItem(spriteBatch, map, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public IItem CreateRupeeItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new RupeeItem(spriteBatch, rupee, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateTriforceItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new TriforceItem(spriteBatch, triforce, startingPos);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateCloudAnimation(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new CloudAnimationSprite(spriteBatch, cloud, startingPos);
        }

        public void SpawnRandomItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            Random rd = new Random();

            int num = rd.Next(0, 15);

            switch (num)
            {
                case 0:
                    CreateBowItem(spriteBatch, startingPos);
                    break;
                case 1:
                    CreateCompassItem(spriteBatch, startingPos);
                    break;
                case 2:
                    CreateBoomerangItem(spriteBatch, startingPos);
                    break;
                case 3:
                    CreateFairyItem(spriteBatch, startingPos);
                    break;
                case 4:
                case 5:
                    CreateBombItem(spriteBatch, startingPos);
                    break;
                case 6:
                    CreateKeyItem(spriteBatch, startingPos);
                    break;
                case 7:
                case 8:
                case 9:
                    CreateHeartItem(spriteBatch, startingPos);
                    break;
                case 10:
                    CreateMapItem(spriteBatch, startingPos);
                    break;
                case 11:
                    CreateHeartContainerItem(spriteBatch, startingPos);
                    break;
                case 12:
                case 13:
                case 14:
                    CreateRupeeItem(spriteBatch, startingPos);
                    break;
            }
        }
    }
}
