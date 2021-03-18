using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Rooms;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class ProjectileCollidable : ICollidable
    {
        private IProjectile projectile;
        private Boolean[] collisionOccurrences;

        public bool DamageDisabled { get; set; }
        
	    public ProjectileCollidable(IProjectile projectile)
        {
            this.projectile = projectile;
            DamageDisabled = true;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is DoorCollidable || collidableObject is WallCollidable || collidableObject is EnemyCollidable)
            {
                RoomProjectiles.Instance.RemoveProjectile(this.projectile);
            }

            if (collidableObject is PlayerCollidable && this.IsEnemy)
            {
                RoomProjectiles.Instance.RemoveProjectile(this.projectile);
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

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Length; i++)
            {
                collisionOccurrences[i] = false;
            }
        }
    }

}