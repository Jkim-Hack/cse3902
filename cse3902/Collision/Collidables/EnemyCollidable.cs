using System;
using cse3902.Interfaces;
using System.Collections;
using cse3902.Projectiles;
using cse3902.Rooms;
using cse3902.Entities.Enemies;
using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902.Collision.Collidables
{
    public class EnemyCollidable : ICollidable
    {
        private IEntity enemy;
        private int damage;
        private Boolean[] collisionOccurrences;
        private int frameCount;
        private ArrayList collisionFrames;

        private bool isDamageDisabled;
        public bool DamageDisabled { get => isDamageDisabled; set => isDamageDisabled = value; }

        public EnemyCollidable(IEntity enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
            this.frameCount = 0;
            collisionFrames = new ArrayList();
            isDamageDisabled = false;
            collisionOccurrences = new Boolean[6];
            this.ResetCollisions();
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is SwordCollidable && !isDamageDisabled)
            {
                SwordCollision(collidableObject);

            } else if (collidableObject is ProjectileCollidable && !isDamageDisabled)
            {
                ProjectileCollision(collidableObject);

            } else if (collidableObject is DoorCollidable || collidableObject is WallCollidable)
            {
                DoorWallCollision(collidableObject);

            } else if (collidableObject is BlockCollidable)
            {
                BlockCollision(collidableObject);
            }

            else
            {
                //no other collision matters for enemies
            }

            
        }

        public void ResetCollisions()
        {
            this.frameCount++;
            if (this.frameCount > 60) { frameCount = 0; }
            for (int i = 0; i < collisionOccurrences.Length-1; i++)
            {
                collisionOccurrences[i] = false;
            }
        }

        private void DirectionLogic()
        {
            if (collisionFrames.Count > 1) { collisionFrames.Clear(); }
            collisionFrames.Add(frameCount);
            if (collisionFrames.Count == 2)
            {
                if ((int)collisionFrames[1] - (int)collisionFrames[0] < 6)
                {
                    Random rand = new System.Random();
                    //get 1 or -1
                    int multiplier = rand.Next(0, 2) * 2 -1;

                    if (this.enemy.Direction.Y != 0)
                    {
                        this.enemy.ChangeDirection(new Vector2(multiplier, 0));
                    }
                    else
                    {
                        this.enemy.ChangeDirection(new Vector2(0, multiplier));
                    }
                    
                }
                else
                {
                    this.enemy.ChangeDirection(new Vector2(0, 0));
                }
            } else
            {
                this.enemy.ChangeDirection(new Vector2(0, 0));
            }
        }

        private void SwordCollision(ICollidable collidableObject)
        {
            if (this.enemy is Dodongo || (this.enemy is Goriya && ((Goriya)this.enemy).IsDetectionMode))
            {
                return;
            }

            this.enemy.TakeDamage(collidableObject.DamageValue);
            if (this.enemy.Health <= 0)
            {
                RoomEnemies.Instance.RemoveEnemy(this.enemy);
            }
            else
            {
                //enemies are not shoved if attack perpdincular to their movement
                if (((SwordCollidable)collidableObject).Direction == this.enemy.Direction || ((SwordCollidable)collidableObject).Direction == -this.enemy.Direction)
                {
                    this.enemy.BeShoved();
                }
            }
        }

        private void ProjectileCollision(ICollidable collidableObject)
        {
            if ((this.enemy is Dodongo && !(((ProjectileCollidable)collidableObject).Projectile is BombProjectile)) || (enemy is Goriya && ((Goriya)enemy).IsDetectionMode))
			{
				return;
			}
            if (((ProjectileCollidable)collidableObject).IsEnemy)
            {
                return;
            }

            if (!(this.enemy is Aquamentus || this.enemy is BoggusBoss || this.enemy is MarioBoss || this.enemy is Dodongo)) {
                if (((ProjectileCollidable)collidableObject).Projectile is BoomerangProjectile)
                {
                    if (this.enemy.Stunned.StunDuration < DamageConstants.BoomerangStunDuration) this.enemy.Stunned = (true, DamageConstants.BoomerangStunDuration);
                }
            }

            this.enemy.TakeDamage(collidableObject.DamageValue);
            if (this.enemy.Health <= 0)
            {
                RoomEnemies.Instance.RemoveEnemy(this.enemy);
                return;
            }
            if (((ProjectileCollidable)collidableObject).DamageValue > 2)
            {
                this.enemy.BeShoved();
            }
        }

        private void DoorWallCollision(ICollidable collidableObject)
        {
            if (collisionOccurrences[0])
            {
                return;
            }

            if (!((this.enemy is WallMaster) || (enemy is Goriya && ((Goriya)enemy).IsDetectionMode)))
            {
                this.enemy.StopShove();
                this.enemy.Center = this.enemy.PreviousCenter;

                DirectionLogic();

                this.collisionOccurrences[0] = true;

            }
        }

        private void BlockCollision(ICollidable collidableObject)
        {
            if (collisionOccurrences[0])
            {
                return;
            }

            if (!(this.enemy is Keese || this.enemy is WallMaster) || (enemy is Goriya && ((Goriya)enemy).IsDetectionMode))
            {
                this.enemy.StopShove();
                this.enemy.Center = this.enemy.PreviousCenter;

                DirectionLogic();

                this.collisionOccurrences[0] = true;
            }
        }

        public ref Rectangle RectangleRef
        {
            get => ref enemy.Bounds;
        }

        public int DamageValue
        {
            get => damage;
        }

        public IEntity Enemy
        {
            get => enemy;
        }
    }
}
