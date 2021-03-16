using System;
using cse3902.Interfaces;
using cse3902.Rooms;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace cse3902.Collision.Collidables
{
    public class PlayerCollidable : ICollidable
    {
        private IPlayer player;
        private int damage;
        private Boolean[] collisionOccurrences;

        public PlayerCollidable(IPlayer player, int damage)
        {
            this.player = player;
            this.damage = damage;
            collisionOccurrences = new Boolean[6];
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collisionOccurrences[0])
            {
                return;
            }


            if (collidableObject is EnemyCollidable)
            {
                //take damage and get shoved back by enemy
                player.TakeDamage(collidableObject.DamageValue);

                if (player.Health <= 0)
                {
                    //remove link from room
                    // TODO: this will need to be changed to reset game and such
                    RoomEnemyNPCs.Instance.RemoveENPC(this.player);
                } else
                {
                    player.BeShoved();
                }
                
            } else if (collidableObject is BlockCollidable || collidableObject is WallCollidable)
            {
                if (collisionOccurrences[0])
                {
                    return;
                }

                if (collidableObject is BlockCollidable)
                {
                    if (!((BlockCollidable)collidableObject).IsWalkable)
                    {
                        player.CenterPosition = player.PreviousPosition;
                        collisionOccurrences[0] = true;
                    }
                } else
                {
                    player.CenterPosition = player.PreviousPosition;
                }
                
                

            } else if (collidableObject is ItemCollidable)
            {
                this.player.AddItem(((ItemCollidable)collidableObject).Item);
                //remove item from room
                RoomItems.Instance.RemoveItem(((ItemCollidable)collidableObject).Item);

            } else if (collidableObject is ProjectileCollidable)
            {
                if (((ProjectileCollidable)collidableObject).IsEnemy)
                {
                    player.TakeDamage(((ProjectileCollidable)collidableObject).DamageValue);
                }
            } else if (collidableObject is DoorCollidable)
            {
                if (collisionOccurrences[0])
                {
                    return;
                }
                if (((DoorCollidable)collidableObject).State == IDoor.DoorState.Closed || ((DoorCollidable)collidableObject).State == IDoor.DoorState.Locked || ((DoorCollidable)collidableObject).State == IDoor.DoorState.Wall)
                {
                    player.CenterPosition = player.PreviousPosition;
                    collisionOccurrences[0] = true;
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