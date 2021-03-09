using System;
using cse3902.Interfaces;
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
            player.TakeDamage(collidableObject.DamageValue);

            //todo: these cases don't currently account for aquamentus fireball
            //should probably just consider it a projectile and check if
            //the projectile is fireball or not
            if (collidableObject is EnemyCollidable)
            {
                //take damage and get shoved back by enemy
                player.TakeDamage(collidableObject.DamageValue);

                if (player.Health <= 0)
                {
                    //todo: destroy object
                } else
                {
                    player.BeShoved();
                }
                
                
            } else if (collidableObject is BlockCollidable)
            {
                //three cases:
                //immovable block
                //movable block
                //staircase

            } else
            {

            }
           
        }

        public ref Rectangle RectangleRef
        {
            get => ref player.Bounds;
        }

        public int DamageValue
        {
            get => damage;
        }
    }
}