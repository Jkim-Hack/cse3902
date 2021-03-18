﻿using System;
using cse3902.Interfaces;
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

        public bool DamageDisabled { get; set; }

        public EnemyCollidable(IEntity enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
            DamageDisabled = false;
            collisionOccurrences = new Boolean[6];
            this.ResetCollisions();
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is SwordCollidable && !DamageDisabled)
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

            } else if (collidableObject is ProjectileCollidable && !DamageDisabled)
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

                    //vector of (0,0) means just change current direction to opposite
                    Vector2 direction = new Vector2(0, 0);
                    this.enemy.ChangeDirection(direction);
                    this.collisionOccurrences[0] = true;
                    //todo: might need to slightly adjust position of entity as well
                }


            } else if (collidableObject is BlockCollidable)
            {
                if (!(this.enemy is Keese))
                {
                    this.enemy.StopShove();

                    //vector of (0,0) means just change current direction to opposite
                    Vector2 direction = new Vector2(0, 0);
                    this.enemy.ChangeDirection(direction);
                    this.collisionOccurrences[0] = true;
                    //todo: might need to slightly adjust position of entity as well
                }
            }

            else
            {
                //no other collision matters for enemies
            }

            
        }

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Length-1; i++)
            {
                collisionOccurrences[i] = false;
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
