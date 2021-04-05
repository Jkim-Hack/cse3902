using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using cse3902.Items;
using cse3902.Rooms;
using cse3902.HUD;
using System;

namespace cse3902.SpriteFactory
{
    public class ItemSpriteFactory
    {
        private SpriteBatch spriteBatch;

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

        private Dictionary<IEntity.EnemyType, List<InventoryManager.ItemType>> dropList;
        private Dictionary<IEntity.EnemyType, int> dropRate;
        private int killCounter;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        // https://www.zeldadungeon.net/zelda-runners-examining-random-and-forced-drops-and-chatting-with-zant/
        private ItemSpriteFactory()
        {
            dropRate = new Dictionary<IEntity.EnemyType, int>();
            dropList = new Dictionary<IEntity.EnemyType, List<InventoryManager.ItemType>>();

            dropRate.Add(IEntity.EnemyType.A, 31);
            dropRate.Add(IEntity.EnemyType.B, 41);
            dropRate.Add(IEntity.EnemyType.C, 59);
            dropRate.Add(IEntity.EnemyType.D, 41);
            dropRate.Add(IEntity.EnemyType.X, 9);

            LoadA();
            LoadB();
            LoadC();
            LoadD();
            LoadX();

            killCounter = 0;
        }

        private void LoadA()
        {
            List<InventoryManager.ItemType> items = new List<InventoryManager.ItemType>();

            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Fairy);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);

            dropList.Add(IEntity.EnemyType.A, items);
        }
        private void LoadB()
        {
            List<InventoryManager.ItemType> items = new List<InventoryManager.ItemType>();

            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Clock);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Heart);

            dropList.Add(IEntity.EnemyType.B, items);
        }
        private void LoadC()
        {
            List<InventoryManager.ItemType> items = new List<InventoryManager.ItemType>();

            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Clock);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Rupee);

            dropList.Add(IEntity.EnemyType.C, items);
        }
        private void LoadD()
        {
            List<InventoryManager.ItemType> items = new List<InventoryManager.ItemType>();

            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Fairy);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Fairy);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);

            dropList.Add(IEntity.EnemyType.D, items);
        }
        private void LoadX()
        {
            List<InventoryManager.ItemType> items = new List<InventoryManager.ItemType>();

            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Bomb);
            items.Add(InventoryManager.ItemType.Fairy);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Clock);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);
            items.Add(InventoryManager.ItemType.Rupee);
            items.Add(InventoryManager.ItemType.Heart);

            dropList.Add(IEntity.EnemyType.X, items);
        }

        public void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

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

        public ISprite CreateBombItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BombItem(spriteBatch, bomb, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateBoomerangItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BoomerangItem(spriteBatch, boomerang, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBoomerangItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BoomerangItem(spriteBatch, boomerang, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateBowItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BowItem(spriteBatch, bow, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateClockItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new ClockItem(spriteBatch, clock, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateCompassItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new CompassItem(spriteBatch, compass, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateFairyItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new FairyItem(spriteBatch, fairy, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateHeartContainerItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartContainerItem(spriteBatch, heartcont, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateHeartContainerItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartContainerItem(spriteBatch, heartcont, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateHeartItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartItem(spriteBatch, heart, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateKeyItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new KeyItem(spriteBatch, key, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateKeyItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new KeyItem(spriteBatch, key, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateMapItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new MapItem(spriteBatch, map, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public IItem CreateRupeeItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new RupeeItem(spriteBatch, rupee, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateTriforceItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new TriforceItem(spriteBatch, triforce, startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }

        public ISprite CreateCloudAnimation(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new CloudAnimationSprite(spriteBatch, cloud, startingPos);
        }

        public void SpawnRandomItem(SpriteBatch spriteBatch, Vector2 startingPos, IEntity.EnemyType type)
        {
            Random rd = new Random();
            int num = rd.Next(0, 100);

            if (num < dropRate[type])
            {
                switch (dropList[type][killCounter % 10])
                {
                    case InventoryManager.ItemType.Rupee:
                        CreateRupeeItem(spriteBatch, startingPos, false, false);
                        break;
                    case InventoryManager.ItemType.Heart:
                        CreateHeartItem(spriteBatch, startingPos, false, false);
                        break;
                    case InventoryManager.ItemType.Bomb:
                        CreateBombItem(spriteBatch, startingPos, false, false);
                        break;
                    case InventoryManager.ItemType.Fairy:
                        CreateFairyItem(spriteBatch, startingPos, false, false);
                        break;
                    case InventoryManager.ItemType.Clock:
                        CreateClockItem(spriteBatch, startingPos, false, false);
                        break;
                    default: //this should never happen
                        break;
                }
            }

            killCounter++;
        }

        public ISprite CreateItemWithType(InventoryManager.ItemType itemType, Vector2 origin)
        {
            switch (itemType)
            {
                case InventoryManager.ItemType.Bomb:
                    return CreateBombItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Boomerang:
                    return CreateBoomerangItem(origin, false, false);
                case InventoryManager.ItemType.Bow:
                    return CreateBowItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Clock:
                    return CreateClockItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Compass:
                    return CreateCompassItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Fairy:
                    return CreateFairyItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Heart:
                    return CreateHeartItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.HeartContainer:
                    return CreateHeartContainerItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Key:
                    return CreateKeyItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Map:
                    return CreateMapItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Rupee:
                    return CreateRupeeItem(spriteBatch, origin, false, false);
                case InventoryManager.ItemType.Triforce:
                    return CreateTriforceItem(spriteBatch, origin, false, false);
                default:
                    return null;
            }
        }
    }
}
