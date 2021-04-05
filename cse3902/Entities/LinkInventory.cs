using cse3902.HUD;
using cse3902.Interfaces;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using cse3902.Sounds;
using cse3902.Items;
using System;

namespace cse3902.Entities
{
    public class LinkInventory
    {
        private LinkStateMachine linkState;
        private SpriteBatch batch;
        private IItem AnimationItem;
        private IProjectile weapon;

        public LinkInventory(Game1 game, LinkStateMachine linkState)
        {
            this.linkState = linkState;
            batch = game.SpriteBatch;
        }

        public void CreateWeapon(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateSwordWeapon(batch, startingPosition, direction, (int)InventoryManager.Instance.SwordSlot);
        }

        public void CreateSwordProjectile(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            if (!RoomProjectiles.Instance.projectiles.Contains(weapon))
            {
                weapon = projectileHandler.CreateSwordItem(batch, startingPosition, direction);
            }
        }

        public void AddItemToInventory(IItem item)
        {
            if (item.Equals(AnimationItem)) return;
            InventoryManager.ItemType type = item.ItemType;
            InventoryManager.Instance.AddToInventory(type);

            //play certain sound
            if (type == InventoryManager.ItemType.Heart || type == InventoryManager.ItemType.Key)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getHeart);
            }
            else if (type == InventoryManager.ItemType.Rupee)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getRupee);
            }
            else
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getItem);
            }

            //update health if certain item
            if (type == InventoryManager.ItemType.Heart)
            {
                linkState.Health += 2;
                if (linkState.Health > linkState.TotalHealth) linkState.Health = linkState.TotalHealth;
            } else if (type == InventoryManager.ItemType.HeartContainer)
            {
                linkState.TotalHealth += 2;
                linkState.Health += 2;
            }

            //pickup animation if certain item
            if (type == InventoryManager.ItemType.Bow)
            {
                Vector2 startingPos = linkState.CollectItemAnimation();
                item.Center = startingPos;
                AnimationItem = item;
                GameStateManager.Instance.LinkPickupItem(36);
            } else if (type == InventoryManager.ItemType.Triforce)
            {
                InventoryManager.Instance.RemoveFromInventory(InventoryManager.ItemType.Compass);
                GameStateManager.Instance.LinkPickupItem(Int32.MaxValue);
                Vector2 startingPos = linkState.GameWonAnimation();
                item.Center = startingPos;
                AnimationItem = item;
                ((TriforceItem)item).GameWon = true;
            }
            else
            {
                RoomItems.Instance.RemoveItem(item);
            }

            //update room condition if certain item
            if (type == InventoryManager.ItemType.Key || type == InventoryManager.ItemType.HeartContainer || type == InventoryManager.ItemType.Boomerang) RoomConditions.Instance.SendSignals();
        }

        public bool CreateItem(Vector2 startingPos)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            //TODO: Remove comment below once implemented inventory system
            //if (InventoryManager.Instance.inventory[InventoryManager.Instance.ItemSlot] == 0) return false;
            //InventoryManager.Instance.inventory[InventoryManager.Instance.ItemSlot]--;
            switch (InventoryManager.Instance.ItemSlot)
            {
                case InventoryManager.ItemType.Bow:
                    projectileHandler.CreateArrowItem(batch, startingPos, linkState.Direction);
                    break;

                case InventoryManager.ItemType.Boomerang:
                    projectileHandler.CreateBoomerangItem(batch, linkState.Sprite, linkState.Direction);
                    break;

                case InventoryManager.ItemType.Bomb:
                    projectileHandler.CreateBombItem(batch, startingPos);
                    break;

                default:
                    // throw new NotImplementedException();
                    break;
            }
            return true;
        }

        public void ChangeWeapon(int index)
        {
            //TODO: Remove comment below once implemented inventory system
            //if((int) InventoryManager.Instance.SwordSlot < index)
            InventoryManager.Instance.SwordSlot = (InventoryManager.SwordType)index;
        }

        public void ChangeItem(InventoryManager.ItemType type)
        {
            InventoryManager.Instance.ItemSlot = type;
        }

        public void RemoveItemAnimation()
        {
            RoomItems.Instance.RemoveItem(AnimationItem);
            AnimationItem = null;
        }
    }
}