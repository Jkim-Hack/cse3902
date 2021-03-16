using System;
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

        public EnemyCollidable(IEntity enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
            collisionOccurrences = new Boolean[6];
            this.ResetCollisions();
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            if (collidableObject is SwordCollidable)
            {
                this.enemy.TakeDamage(collidableObject.DamageValue);
                if (this.enemy.Health <= 0)
                {
                    RoomEnemyNPCs.Instance.RemoveENPC(this.enemy);
                } else
                {
                    //enemies are not shoved if attack perpdincular to their movement
                    if (((SwordCollidable)collidableObject).Direction == this.enemy.Direction || ((SwordCollidable)collidableObject).Direction == -this.enemy.Direction)
                    {
                        this.enemy.BeShoved();
                    }
                    
                }

            } else if (collidableObject is ProjectileCollidable)
            {
                if (this.enemy is Gel || this.enemy is Keese)
                {
                    //only gels and keese take damage from projectiles it seems
                    this.enemy.TakeDamage(collidableObject.DamageValue);
                    if (this.enemy.Health <= 0)
                    {
                        RoomEnemyNPCs.Instance.RemoveENPC(this.enemy);
                    }
                } else
                {
                    //other enemies are simply stunned in place for a bit
                    //need some kind of method to be able to 'stun' the enemies
                    //they will still animate, just not move
                }
            } else if (!collisionOccurrences[0] && (collidableObject is BlockCollidable || collidableObject is DoorCollidable || collidableObject is DoorCollidable))
            {
                if (!(this.enemy is WallMaster))
                {
                    //vector of (0,0) means just change current direction to opposite
                    Vector2 direction = new Vector2(0, 0);
                    this.enemy.ChangeDirection(direction);
                    this.collisionOccurrences[0] = true;
                    //todo: might need to slightly adjust position of entity as well
                }


            } else
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
