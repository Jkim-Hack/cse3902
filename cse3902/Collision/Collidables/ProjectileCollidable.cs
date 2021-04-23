using System;
using cse3902.Interfaces;
using cse3902.Entities.Enemies;
using cse3902.Rooms;
using cse3902.Projectiles;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class ProjectileCollidable : ICollidable
    {
        private IProjectile projectile;
        private Boolean collisionOccurrence;
        private Game1 game;

        public bool DamageDisabled { get; set; }
        
	    public ProjectileCollidable(IProjectile projectile, Game1 game)
        {
            this.projectile = projectile;
            DamageDisabled = true;
            collisionOccurrence = false;
            this.game = game;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is EnemyCollidable)
            {
                if (((EnemyCollidable)collidableObject).Enemy is Goriya)
                {
                    if (((Goriya)((EnemyCollidable)collidableObject).Enemy).IsDetectionMode) return;
                }

                if (((EnemyCollidable)collidableObject).Enemy is WallMaster) {
                    if (!((WallMaster)((EnemyCollidable)collidableObject).Enemy).IsTriggered) return;
                }
            }

            if (this.projectile is BoomerangProjectile) BoomerangCollision(collidableObject);
            else if (this.projectile is EnemyBoomerangProjectile)
            {
                if (collidableObject is DoorCollidable || collidableObject is WallCollidable || collidableObject is PlayerCollidable) ((EnemyBoomerangProjectile)projectile).ReverseDirectionIfNecessary();
            }
            else
            {
                if (collidableObject is DoorCollidable || collidableObject is WallCollidable || (collidableObject is EnemyCollidable && !this.IsEnemy))
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
        }

        private void BoomerangCollision(ICollidable collidableObject)
        {

            if (collidableObject is ItemCollidable) game.Player.AddItem(((ItemCollidable)collidableObject).Item);
            else if (collidableObject is DoorCollidable || collidableObject is WallCollidable || collidableObject is EnemyCollidable)
            {
                ((BoomerangProjectile)projectile).ReverseDirectionIfNecessary();
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
                if (this.projectile is Fireball || this.projectile is EnemyBoomerangProjectile)
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
            collisionOccurrence = false;
            
        }

        public IProjectile Projectile
        {
            get => projectile;
        }
    }

}
