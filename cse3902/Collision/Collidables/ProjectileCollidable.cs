﻿using System;
using cse3902.Interfaces;
using cse3902.Rooms;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class ProjectileCollidable : ICollidable
    {
        private IProjectile projectile;

        public ProjectileCollidable(IProjectile projectile)
        {
            this.projectile = projectile;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (!(collidableObject is ProjectileCollidable || collidableObject is SwordCollidable))
            {
                //projectiles also implement IItem, can cast them to item to remove
                RoomItems.Instance.RemoveItem((IItem)this.projectile);
            }


        }

        public ref Rectangle RectangleRef
        {
            get => ref projectile.Box;
        }

        public Boolean IsEnemy
        {
            get
            {
                if (this.projectile is Fireball)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public int DamageValue
        {
            get => projectile.Damage;
        }
    }

}