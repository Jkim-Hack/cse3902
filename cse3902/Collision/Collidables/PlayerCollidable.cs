using System;
using cse3902.Interfaces;
using cse3902.Rooms;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class PlayerCollidable : ICollidable
    {
        private IPlayer player;
        private int damage;

        public PlayerCollidable(IPlayer player, int damage)
        {
            this.player = player;
            this.damage = damage;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

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
                if (collidableObject is BlockCollidable)
                {
                    if (!((BlockCollidable)collidableObject).IsWalkable)
                    {
                        player.CenterPosition = player.PreviousPosition;
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

        public int DamageValue
        {
            get => damage;
        }
    }
}