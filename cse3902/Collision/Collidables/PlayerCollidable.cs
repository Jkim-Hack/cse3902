using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Entities.Enemies;

namespace cse3902.Collision.Collidables
{
    public class PlayerCollidable : ICollidable
    {
        private Game1 game;

        private IPlayer player;
        private int damage;
        private Boolean[] collisionOccurrences;

        private bool isDamageDisabled;
        public bool DamageDisabled { get => isDamageDisabled; set => isDamageDisabled = value; }

        public PlayerCollidable(IPlayer player, int damage, Game1 game)
        {
            this.player = player;
            this.damage = damage;
            isDamageDisabled = false;
            collisionOccurrences = new Boolean[6];
            this.game = game;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

            if (collidableObject is EnemyCollidable && !isDamageDisabled)
            {
                EnemyCollision(collidableObject);

            }
            else if (collidableObject is BlockCollidable || collidableObject is WallCollidable)
            {
                WallBlockCollision(collidableObject);
            }
            else if (collidableObject is ItemCollidable)
            {
                this.player.AddItem(((ItemCollidable)collidableObject).Item);

            }
            else if (collidableObject is ProjectileCollidable)
            {
                ProjectileCollision(collidableObject);
            }
            else if (collidableObject is DoorCollidable)
            {
                DoorCollision(collidableObject);
            }
            else if (collidableObject is TrapCollidable)
            {
                TrapCollision(collidableObject);
            }
        }

        public ref Rectangle RectangleRef
        {
            get => ref player.Bounds;
        }

        public Vector2 Direction
        {
            get => player.Direction;
        }

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Length - 1; i++)
            {
                collisionOccurrences[i] = false;
            }
        }

        public int DamageValue
        {
            get => damage;
        }

        private void EnemyCollision(ICollidable collidableObject)
        {           
            if (((EnemyCollidable)collidableObject).Enemy is WallMaster)
            {
                if (((WallMaster)((EnemyCollidable)collidableObject).Enemy).IsTriggered)
                {
                    player.TakeDamage(collidableObject.DamageValue);
                    if (player.Health <= 0)
                    {
                        this.player.Die();
                        return;
                    }
                    ((WallMaster)((EnemyCollidable)collidableObject).Enemy).GrabLink();
                    GameStateManager.Instance.LinkGrabbedByWallMaster(120);
                    

                }
                else
                {
                    ((WallMaster)((EnemyCollidable)collidableObject).Enemy).IsTriggered = true;
                }

            }
            else
            {
                //take damage and get shoved back by enemy  
                player.TakeDamage(collidableObject.DamageValue);

                if (player.Health <= 0)
                {
                    this.player.Die();
                }
                else
                {
                    player.BeShoved();
                }
            }
        }

        private void WallBlockCollision(ICollidable collidableObject)
        {
            if (collisionOccurrences[0])
            {
                return;
            }

            if (collidableObject is BlockCollidable)
            {
                if (!((BlockCollidable)collidableObject).IsWalkable)
                {
                    player.Center = player.PreviousCenter;
                    collisionOccurrences[0] = true;
                }
            }
            else
            {
                if (!player.IsGrabbed)
                {
                    player.Center = player.PreviousCenter;
                    collisionOccurrences[0] = true;
                }
                else
                {
                    GameStateManager.Instance.LinkGrabbedByWallMaster(1);
                    player.IsGrabbed = false;
                }

            }
        }

        private void ProjectileCollision(ICollidable collidableObject)
        {

            if (((ProjectileCollidable)collidableObject).IsEnemy && !isDamageDisabled)
            {
                player.TakeDamage(((ProjectileCollidable)collidableObject).DamageValue);
                if (player.Health <= 0)
                {
                    this.player.Die();
                }
                else
                {
                    player.BeShoved();
                }
            }

        }

        private void DoorCollision(ICollidable collidableObject)
        {
            if (collisionOccurrences[0])
            {
                return;
            }
            if (((DoorCollidable)collidableObject).State == IDoor.DoorState.Closed || ((DoorCollidable)collidableObject).State == IDoor.DoorState.Locked || ((DoorCollidable)collidableObject).State == IDoor.DoorState.Wall)
            {
                player.Center = player.PreviousCenter;
                collisionOccurrences[0] = true;
            }

        }

        private void TrapCollision(ICollidable collidableObject)
        {
            if (((TrapCollidable)collidableObject).Trap.IsTriggered)
            {
                //take damage and get shoved back by enemy
                if (!isDamageDisabled)
                {
                    player.TakeDamage(collidableObject.DamageValue);

                    if (player.Health <= 0)
                    {
                        this.player.Die();
                    }
                    else
                    {
                        player.BeShoved();
                    }

                }



            }
            else
            {
                ((TrapCollidable)collidableObject).Trap.Trigger();
            }
        }


    }
}