using cse3902.HUD;
using cse3902.Interfaces;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using Microsoft.Xna.Framework.Audio;
using cse3902.Sounds;
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
            if (item.ItemType == InventoryManager.ItemType.Heart || item.ItemType == InventoryManager.ItemType.Key)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getHeart);
            }
            else if (item.ItemType == InventoryManager.ItemType.Rupee)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getRupee);
            }
            else
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getItem);
            }

            if (item.Equals(AnimationItem)) return;
            InventoryManager.ItemType type = item.ItemType;
            InventoryManager.Instance.AddToInventory(type);
            if (type == InventoryManager.ItemType.Triforce || type == InventoryManager.ItemType.Bow)
            {
                //The basic logic to use item. needs to add Pause Game during the duration and such..
                Vector2 startingPos = linkState.CollectItemAnimation();
                item.Center = startingPos;
                AnimationItem = item;
                GameStateManager.Instance.LinkPickupItem(36);
            }
            else
            {
                RoomItems.Instance.RemoveItem(item);
            }
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
                    throw new NotImplementedException();
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