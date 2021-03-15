﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class SwordCollidable : ICollidable
    {
        private IProjectile sword;
        private int damage;

        public SwordCollidable(IProjectile projectile, int damage)
        {
            this.sword = projectile;
            this.damage = damage;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //nothing actually happend TO swords upon collision
            //so do nothing here
        }

        public ref Rectangle RectangleRef
        {
            get => ref sword.Box;
        }
             
        public int DamageValue
        {
            get => damage;
        }

        public Vector2 Direction
        {
            get => this.sword.Direction;
        }
    }

}