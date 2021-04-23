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

        private Dictionary<String,Texture2D> textures;

        private Dictionary<IEntity.EnemyType, List<InventoryManager.ItemType>> dropList;
        private Dictionary<IEntity.EnemyType, SettingsValues.Variable> dropRate;
        private int killCounter;

        private ItemLoader itemLoader;

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
            textures = new Dictionary<String, Texture2D>();
            dropRate = new Dictionary<IEntity.EnemyType, SettingsValues.Variable>();
            dropList = new Dictionary<IEntity.EnemyType, List<InventoryManager.ItemType>>();

            dropRate.Add(IEntity.EnemyType.A, SettingsValues.Variable.ItemDropA);
            dropRate.Add(IEntity.EnemyType.B, SettingsValues.Variable.ItemDropB);
            dropRate.Add(IEntity.EnemyType.C, SettingsValues.Variable.ItemDropC);
            dropRate.Add(IEntity.EnemyType.D, SettingsValues.Variable.ItemDropD);
            dropRate.Add(IEntity.EnemyType.X, SettingsValues.Variable.ItemDropX);

            itemLoader = new ItemLoader();
          
            killCounter = 0;
        }

        public void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            textures.Add("sword", content.Load<Texture2D>("SwordItems"));
            textures.Add("arrow", content.Load<Texture2D>("arrow"));
            textures.Add("bluePotion", content.Load<Texture2D>("BluePotion"));
            textures.Add("blueRing", content.Load<Texture2D>("BlueRing"));
            textures.Add("bomb", content.Load<Texture2D>("bomb"));
            textures.Add("boomerang", content.Load<Texture2D>("boomerang"));
            textures.Add("bow", content.Load<Texture2D>("bow"));
            textures.Add("clock", content.Load<Texture2D>("clock"));
            textures.Add("fairy", content.Load<Texture2D>("fairy"));
            textures.Add("compass", content.Load<Texture2D>("compass"));
            textures.Add("heart", content.Load<Texture2D>("heart"));
            textures.Add("heartcont", content.Load<Texture2D>("heartcont"));
            textures.Add("triforce", content.Load<Texture2D>("triforce"));
            textures.Add("key", content.Load<Texture2D>("key"));
            textures.Add("magicBook", content.Load<Texture2D>("MagicBook"));
            textures.Add("map", content.Load<Texture2D>("map"));
            textures.Add("rupee", content.Load<Texture2D>("rupee"));
            textures.Add("cloud", content.Load<Texture2D>("cloud"));

            itemLoader.LoadA();
            itemLoader.LoadB();
            itemLoader.LoadC();
            itemLoader.LoadD();
            itemLoader.LoadX();
        }

        // Only used for HUD
        public ISprite CreateArrowItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IItem add = new ArrowItem(spriteBatch, textures["arrow"], startingPos, new Vector2(0, 1));
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public IItem CreateSwordItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept, int type)
        {
            IItem add = new SwordItem(this.spriteBatch, textures["sword"], startingPos, kept, resetKept, type);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBluePotionItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BluePotionItem(spriteBatch, textures["bluePotion"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBlueRingItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BlueRingItem(spriteBatch, textures["blueRing"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBombItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BombItem(spriteBatch, textures["bomb"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBoomerangItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BoomerangItem(spriteBatch, textures["boomerang"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBoomerangItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BoomerangItem(spriteBatch, textures["boomerang"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateBowItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new BowItem(spriteBatch, textures["bow"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateClockItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new ClockItem(spriteBatch, textures["clock"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateCompassItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new CompassItem(spriteBatch, textures["compass"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateFairyItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new FairyItem(spriteBatch, textures["fairy"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateHeartContainerItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartContainerItem(spriteBatch, textures["heartcont"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateHeartContainerItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartContainerItem(spriteBatch, textures["heartcont"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateHeartItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartItem(spriteBatch, textures["heart"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateHeartItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new HeartItem(spriteBatch, textures["heart"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateKeyItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new KeyItem(spriteBatch, textures["key"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateKeyItem(Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new KeyItem(spriteBatch, textures["key"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public IItem CreateMagicBookItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new MagicBookItem(this.spriteBatch, textures["magicBook"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateMapItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new MapItem(spriteBatch, textures["map"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public IItem CreateRupeeItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new RupeeItem(spriteBatch, textures["rupee"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateTriforceItem(SpriteBatch spriteBatch, Vector2 startingPos, bool kept, bool resetKept)
        {
            IItem add = new TriforceItem(spriteBatch, textures["triforce"], startingPos, kept, resetKept);
            RoomItems.Instance.AddItem(add);
            return add;
        }
        public ISprite CreateCloudAnimation(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new CloudAnimationSprite(spriteBatch, textures["cloud"], startingPos);
        }

        public Dictionary<IEntity.EnemyType, List<InventoryManager.ItemType>> DropList
        {
            get => dropList;
        }

        public void SpawnRandomItem(SpriteBatch spriteBatch, Vector2 startingPos, IEntity.EnemyType type)
        {
            Random rd = new Random();
            int num = rd.Next(0, 100);

            if (num < SettingsValues.Instance.GetValue(dropRate[type]))
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
                case InventoryManager.ItemType.Arrow:
                    return CreateArrowItem(spriteBatch, origin);
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
