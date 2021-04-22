using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.HUD;
using cse3902.SpriteFactory;

namespace cse3902.Items
{
    public class ItemLoader
    {
        public ItemLoader()
        {
        }

        public void LoadA()
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

            ItemSpriteFactory.Instance.DropList.Add(IEntity.EnemyType.A, items);
        }

        public void LoadB()
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

            ItemSpriteFactory.Instance.DropList.Add(IEntity.EnemyType.B, items);
        }

        public void LoadC() //stalfos, wallmaster
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

            ItemSpriteFactory.Instance.DropList.Add(IEntity.EnemyType.C, items);
        }

        public void LoadD() //aquamentus, goriya
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

            ItemSpriteFactory.Instance.DropList.Add(IEntity.EnemyType.D, items);
        }

        public void LoadX() //keese, gel
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

            ItemSpriteFactory.Instance.DropList.Add(IEntity.EnemyType.X, items);
        }
    }
}
