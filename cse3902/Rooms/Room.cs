using cse3902.Interfaces;
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
        public Vector3 RoomPos { get => roomPos; }

        private List<IItem> items;
        private List<IEntity> enemies;
        private List<INPC> npcs;
        private List<IProjectile> projectiles;
        private List<IBlock> blocks;
        private List<IDoor> doors;

        public Room(Vector3 position, int spriteNum)
        {
            roomPos = position;
           
            visited = false;

            if (spriteNum < 0)
            {
                Background.Instance.generateItemRoom(position);
            }
            else
            {
                Background.Instance.generateRoom(position, spriteNum);
            }

            items = new List<IItem>();
            enemies = new List<IEntity>();
            npcs = new List<INPC>();
            projectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
            doors = new List<IDoor>();
        }

        public void AddItem (IItem item)
        {
            items.Add(item);
        }

        public void AddEnemy(IEntity enemy)
        {
            enemies.Add(enemy);
        }

        public void AddNPC(INPC npc)
        {
            npcs.Add(npc);
        }

        public void AddProjectile(IProjectile projectile)
        {
            projectiles.Add(projectile);
        }

        public void AddBlock(IBlock block)
        {
            blocks.Add(block);
        }

        public void AddDoor(IDoor door)
        {
            doors.Add(door);
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

        public List<INPC> NPCs
        {
            get => npcs;
            set => npcs = value;
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

        public List<IDoor> Doors
        {
            get => doors;
            set => doors = value;
        }
    }
}
