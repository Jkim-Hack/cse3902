using System;
using cse3902.Interfaces;
using System.Collections;
using System.Collections.Generic;
using cse3902.Rooms;
using cse3902.Entities.Enemies;
using Microsoft.Xna.Framework;

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
                this.enemy.TakeDamage(collidableObject.DamageValue);
                if (this.enemy.Health <= 0)
                {
                    RoomEnemies.Instance.RemoveEnemy(this.enemy);
                } else
                {
                    //enemies are not shoved if attack perpdincular to their movement
                    if (((SwordCollidable)collidableObject).Direction == this.enemy.Direction || ((SwordCollidable)collidableObject).Direction == -this.enemy.Direction)
                    {
                        this.enemy.BeShoved();
                    }
                }

            } else if (collidableObject is ProjectileCollidable && !isDamageDisabled)
            {
                if (this.enemy is Gel || this.enemy is Keese)
                {
                    //only gels and keese take damage from projectiles it seems
                    this.enemy.TakeDamage(collidableObject.DamageValue);
                    if (this.enemy.Health <= 0)
                    {
                        RoomEnemies.Instance.RemoveEnemy(this.enemy);
                    }
                } else
                {

                    if (((ProjectileCollidable)collidableObject).DamageValue > 5)
                    {
                        this.enemy.TakeDamage(collidableObject.DamageValue);
                        this.enemy.BeShoved();
                        if (this.enemy.Health <= 0)
                        {
                            RoomEnemies.Instance.RemoveEnemy(this.enemy);
                        }
                    }
                    //other enemies are simply stunned in place for a bit
                    //need some kind of method to be able to 'stun' the enemies
                    //they will still animate, just not move
                }
            } else if (collidableObject is DoorCollidable || collidableObject is WallCollidable)
            {
                if (collisionOccurrences[0])
                {
                    return;
                }

                if (!(this.enemy is WallMaster))
                {
                    this.enemy.StopShove();
                    this.enemy.Center = this.enemy.PreviousCenter;

                    DirectionLogic();

                    this.collisionOccurrences[0] = true;

                }


            } else if (collidableObject is BlockCollidable)
            {
                if (collisionOccurrences[0])
                {
                    return;
                }

                if (!(this.enemy is Keese))
                {
                    this.enemy.StopShove();
                    this.enemy.Center = this.enemy.PreviousCenter;

                    DirectionLogic();

                    this.collisionOccurrences[0] = true;
                }
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
                    int choice = rand.Next(0, 2);
                    int multiplier = 0;

                    if (choice == 0)
                    {
                        multiplier = 1;
                    } else
                    {
                        multiplier = -1;
                    }

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

        public ref Rectangle RectangleRef
        {
            get => ref enemy.Bounds;
        }

        public int DamageValue
        {
            get => damage;
        }

        
        
    }
}
