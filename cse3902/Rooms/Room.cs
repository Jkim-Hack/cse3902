﻿using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using cse3902.Entities;
using System;

namespace cse3902.Rooms
{
    public class Room
    {
        private bool visited;
      
        private Vector3 roomPos;

        private List<IItem> items { get; set; }
        private List<IEntity> enemies { get; set; }
        private List<IProjectile> projectiles { get; set; }
        private List<IBlock> blocks { get; set; }

        public Room(Vector3 position)
        {
            roomPos = position;
           
            visited = false;

            items = new List<IItem>();
            enemies = new List<IEntity>();
            projectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
        }

        public void AddItem (IItem item)
        {
            items.Add(item);
        }

        public void AddEnemy(IEntity enemy)
        {
            enemies.Add(enemy);
        }

        public void AddProjectile(IProjectile projectile)
        {
            projectiles.Add(projectile);
        }

        public bool IsVisited()
        {
            return visited;
        }

        public void SetToVisited()
        {
            visited = true;
        }

        public List<IItem> Items
        {
            get => items;
            set => items = value;
        }

        public List<IEntity> Enemies
        {
            get => enemies;
            set => enemies = value;
        }

        public List<IProjectile> Projectiles
        {
            get => projectiles;
            set => projectiles = value;
        }

        public List<IBlock> Blocks
        {
            get => blocks;
            set => blocks = value;
        }
    }
}
