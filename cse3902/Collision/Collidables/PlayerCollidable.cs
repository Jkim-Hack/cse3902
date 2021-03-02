using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class PlayerCollidable : ICollidable
    {
        private IEntity player;
        private int damage;

        public PlayerCollidable(IEntity player, int damage)
        {
            this.player = player;
            this.damage = damage;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            player.TakeDamage(collidableObject.DamageValue);

            if (collidableObject is EnemyCollidable)
            {
                //take damage and get shoved back by enemy
                player.TakeDamage(collidableObject.DamageValue);
                
            }
            //if collidableObject is enemy, don't take damage, don't move
            //if collidableObject is player, don't take damage don't move
            //ii collidableObject is weapon, take damage and move
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