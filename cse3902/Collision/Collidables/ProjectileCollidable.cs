using System;
using cse3902.Interfaces;
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
            get => this.projectile is Fireball || this.projectile is EnemyBoomerangProjectile;
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