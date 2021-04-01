using cse3902.Interfaces;
using cse3902.Projectiles;
using cse3902.Sprites;
using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using cse3902.SpriteFactory;
using cse3902.Rooms;

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
            projectileHandler.CreateSwordWeapon(batch, startingPosition, direction, (int) InventoryManager.Instance.SwordSlot);
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
            if (type == InventoryManager.ItemType.Triforce || type == InventoryManager.ItemType.Bow)
            {
                //The basic logic to use item. needs to add Pause Game during the duration and such..
                Vector2 startingPos = linkState.CollectItemAnimation();
                item.Center = startingPos;
                AnimationItem = item;
                GameStateManager.Instance.LinkPickupItem(36);
            } else
            {
                RoomItems.Instance.RemoveItem(item);
            }
        }

        public void CreateItem(Vector2 startingPos)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            IProjectile projectile;
            switch (InventoryManager.Instance.ItemSlot)
            {
                case InventoryManager.ItemType.Bow:
                    projectile =  projectileHandler.CreateArrowItem(batch, startingPos, linkState.Direction);
                    break;

                case InventoryManager.ItemType.Boomerang:
                    projectile =  projectileHandler.CreateBoomerangItem(batch, linkState.Sprite, linkState.Direction);
                    break;

                case InventoryManager.ItemType.Bomb:
                    projectile =  projectileHandler.CreateBombItem(batch, startingPos);
                    break;

                default:
                    projectile = null;
                    break;
            }

        }
        public void ChangeWeapon(int index)
        {
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