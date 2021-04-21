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
        private Boolean collisionOccurrence;
        private bool isEnemy;

        public bool DamageDisabled { get; set; }
        
	    public ProjectileCollidable(IProjectile projectile)
        {
            this.projectile = projectile;
            DamageDisabled = true;
            collisionOccurrence = false;

            isEnemy = this.projectile is Fireball;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is DoorCollidable || collidableObject is WallCollidable || collidableObject is EnemyCollidable)
            {
                if (collisionOccurrence)
                {
                    return;
                }
                collisionOccurrence = true;
                RoomProjectiles.Instance.RemoveProjectile(this.projectile);
            }

            if (collidableObject is PlayerCollidable && this.IsEnemy)
            {
                if (collisionOccurrence)
                {
                    return;
                }
                collisionOccurrence = true;
                RoomProjectiles.Instance.RemoveProjectile(this.projectile);
            }
        }

        public ref Rectangle RectangleRef
        {
            get => ref projectile.Box;
        }

        public Boolean IsEnemy
        {
            get => isEnemy;
            set => isEnemy = value;
        }

        public int DamageValue
        {
            get => projectile.Damage;
        }

        public void ResetCollisions()
        {
            collisionOccurrence = false;
            
        }

        public IProjectile Projectile
        {
            get => projectile;
        }
    }

}