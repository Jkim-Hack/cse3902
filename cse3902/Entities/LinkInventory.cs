﻿using cse3902.Interfaces;
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
        private Game1 game;
        private SpriteBatch batch;
        private IItem AnimationItem;
        private IProjectile weapon;

        private int currItemIndex;
        private int currWeaponIndex;


        public LinkInventory(Game1 game, LinkStateMachine linkState)
        {
            this.linkState = linkState;
            this.game = game;
            batch = game.SpriteBatch;

            currWeaponIndex = 0;
            currItemIndex = 0;
        }


        public void Update(GameTime gameTime)
        {
             
        }

        public void ChangeWeapon(int index)
        {
            InventoryManager.Instance.SlotA = (InventoryManager.SwordType) index;
        }

        public void CreateWeapon(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateSwordWeapon(batch, startingPosition, direction, (int) InventoryManager.Instance.SlotA);
        }

        public void CreateSwordProjectile(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            //Vector2 startingPosition = linkState.UseItemAnimation();
            if (startingPosition.Equals(new Vector2(-1, -1))) return;
            weapon = projectileHandler.CreateSwordItem(batch, startingPosition, direction);
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

        public void UseItem()
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            if (RoomProjectiles.Instance.projectiles.Contains(weapon) && currItemIndex == 1) return;
            Vector2 startingPosition = linkState.UseItemAnimation();
            if (startingPosition.Equals(new Vector2(-1, -1))) return;
            IProjectile projectile;
            switch (currItemIndex)
            {
                case 1:
                    weapon = projectileHandler.CreateSwordItem(batch, startingPosition, linkState.Direction);
                    break;

                case 2:
                    projectile =  projectileHandler.CreateArrowItem(batch, startingPosition, linkState.Direction);
                    break;

                case 3:
                    projectile =  projectileHandler.CreateBoomerangItem(batch, linkState.Sprite, linkState.Direction);
                    break;

                case 4:
                    projectile =  projectileHandler.CreateBombItem(batch, startingPosition);
                    break;

                default:
                    projectile = null;
                    break;
            }

        }

        public void ChangeItem(int index)
        {
            currItemIndex = index;
        }

        public void RemoveItemAnimation()
        {
            RoomItems.Instance.RemoveItem(AnimationItem);
            AnimationItem = null;
        }
    }
}