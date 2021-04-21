using cse3902.HUD;
using cse3902.Interfaces;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using cse3902.Sounds;
using cse3902.Items;
using cse3902.Constants;
using cse3902.Utilities;
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
            projectileHandler.CreateSwordWeapon(batch, startingPosition, direction, InventoryUtilities.convertSwordToInt(InventoryManager.Instance.SwordSlot));
        }
        public void CreateMagicBeam(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            projectileHandler.CreateSwordWeapon(batch, startingPosition, direction, InventoryUtilities.convertSwordToInt(InventoryManager.ItemType.MagicalRod));
            projectileHandler.CreateMagicBeam(batch, startingPosition, direction);
        }

        public void CreateSwordProjectile(Vector2 startingPosition, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            if (!RoomProjectiles.Instance.projectiles.Contains(weapon))
            {
                weapon = projectileHandler.CreateSwordItem(batch, startingPosition, direction);
            }
        }

        //TODO break apart
        public void AddItemToInventory(IItem item)
        {
            if (item.Equals(AnimationItem)) return;
            InventoryManager.ItemType type = item.ItemType;
            InventoryManager.Instance.AddToInventory(type);
            
            //special effect
            if (type == InventoryManager.ItemType.Heart)
            {
                linkState.Health += HeartConstants.HeartItemAddition;
            } else if (type == InventoryManager.ItemType.HeartContainer)
            {
                linkState.TotalHealth += HeartConstants.HeartContainerItemAddition;
                linkState.Health += HeartConstants.HeartContainerItemAddition;
            } else if (type == InventoryManager.ItemType.Fairy)
            {
                linkState.Health = linkState.TotalHealth;
            }

            //pickup animation if certain item
            if (type == InventoryManager.ItemType.Bow || InventoryUtilities.SwordsList.Contains(type))
            {
                Vector2 startingPos = linkState.CollectItemAnimation();
                item.Center = startingPos;
                AnimationItem = item;
              
                GameStateManager.Instance.LinkPickupItem(96, false);
                SoundFactory.Instance.fanfare.Play();
            } 
            else if (type == InventoryManager.ItemType.Triforce)
            {
                linkState.Health = linkState.TotalHealth;
                InventoryManager.Instance.RemoveFromInventory(InventoryManager.ItemType.Compass);
                GameStateManager.Instance.LinkPickupItem(601, true);
                VisionBlocker.Instance.Triforce = true;
                Vector2 startingPos = linkState.GameWonAnimation();
                item.Center = startingPos;
                AnimationItem = item;
                ((TriforceItem)item).GameWon = true;
                SoundFactory.PlaySound(SoundFactory.Instance.getItem, 0.25f);
                SoundFactory.PlaySound(SoundFactory.Instance.triforce, 0.25f);
            }
            else
            {
                RoomItems.Instance.RemoveItem(item);
            }

            //update room condition if certain item
            if (type == InventoryManager.ItemType.Key || type == InventoryManager.ItemType.HeartContainer || type == InventoryManager.ItemType.Boomerang) RoomConditions.Instance.SendSignals();
        }

        public bool CreateItem(Vector2 startingPos, Vector2 direction)
        {
            ProjectileHandler projectileHandler = ProjectileHandler.Instance;
            InventoryManager.ItemType type = InventoryManager.Instance.ItemSlot;
            if (type == InventoryManager.ItemType.None) return false;
            InventoryManager.ItemType decType = type;
            if (type == InventoryManager.ItemType.Arrow)
            {
                decType = InventoryManager.ItemType.Rupee;
            }
            if (InventoryManager.Instance.ItemCount(decType) == 0) return false;
            InventoryManager.Instance.RemoveFromInventory(decType);
            switch (type)
            {
                case InventoryManager.ItemType.Arrow:
                    projectileHandler.CreateArrowItem(batch, startingPos, direction);
                    break;

                case InventoryManager.ItemType.Boomerang:
                    projectileHandler.CreateBoomerangItem(batch, linkState.Sprite, direction);
                    break;

                case InventoryManager.ItemType.Bomb:
                    projectileHandler.CreateBombItem(batch, startingPos);
                    break;

                case InventoryManager.ItemType.MagicalRod:
                    CreateMagicBeam(startingPos, direction);
                    break;

                case InventoryManager.ItemType.BluePotion:
                    linkState.Health = linkState.TotalHealth;
                    break;
                default:
                    // throw new NotImplementedException();
                    break;
            }
            return true;
        }

        public void ChangeWeapon(int index)
        {
            InventoryManager.ItemType type = InventoryUtilities.convertIntToSword(index);
            if (!InventoryUtilities.SwordsList.Contains(type)) return;
            if (InventoryUtilities.convertSwordToInt(InventoryManager.Instance.SwordSlot) < index)
            InventoryManager.Instance.SwordSlot = InventoryUtilities.convertIntToSword(index);
        }
        public int updateDamage(int damage)
        {
            if(InventoryManager.Instance.ItemCount(InventoryManager.ItemType.BlueRing) > 0)
            {
                return damage / 2;
            }
            return damage;
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