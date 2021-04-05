using System;
using cse3902.Interfaces;
using cse3902.Rooms;
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
		        if (((EnemyCollidable)collidableObject).Enemy is WallMaster)
                {
                    if (((WallMaster)((EnemyCollidable)collidableObject).Enemy).IsTriggered) 
                    {
                        player.TakeDamage(collidableObject.DamageValue);
                        //todo: magic number
                        player.BeGrabbed((WallMaster)((EnemyCollidable)collidableObject).Enemy, 30.0f);
                        //GameStateManager.Instance.LinkGrabbedByWallMaster(1);

                    } else
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
                    player.BeShoved();
                }
                
            } 
	        else if (collidableObject is BlockCollidable || collidableObject is WallCollidable)
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
                    } else
                    {
                        GameStateManager.Instance.LinkGrabbedByWallMaster(1);
                        player.IsGrabbed = false;
                    }
                    
                } 
            } 
	        else if (collidableObject is ItemCollidable)
            {
                this.player.AddItem(((ItemCollidable)collidableObject).Item);

            } 
	        else if (collidableObject is ProjectileCollidable)
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
	        else if (collidableObject is DoorCollidable)
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
            else if (collidableObject is TrapCollidable)
            {
                if (((TrapCollidable)collidableObject).Trap.IsTriggered)
                {
                    //take damage and get shoved back by enemy
                    if (!isDamageDisabled) {
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
                    
                    

                } else
                {
                    ((TrapCollidable)collidableObject).Trap.Trigger();
                }
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
    }
}