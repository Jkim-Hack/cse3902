using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using cse3902.Items;

namespace cse3902.Rooms
{
    public class Room
    {
        private bool visited;

        private Vector3 roomPos;

        private List<IItem> items;
        private List<IEntity> enemies;
        private List<IEntity> originalEnemies;
        private List<ITrap> originalTraps;
        private List<ITrap> traps;
        private List<INPC> npcs;
        private List<IProjectile> projectiles;
        private List<IBlock> blocks;
        private List<IDoor> doors;
        private List<ICondition> conditions;
        private List<ISpawner> spawners;

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
            originalEnemies = new List<IEntity>();
            originalTraps = new List<ITrap>();
            traps = new List<ITrap>();
            npcs = new List<INPC>();
            projectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
            doors = new List<IDoor>();
            conditions = new List<ICondition>();
            spawners = new List<ISpawner>();
        }
        public Vector3 RoomPos { get => roomPos; }

        public void AddItem (IItem item)
        {
            items.Add(item);
        }

        public void AddEnemy(IEntity enemy)
        {
            enemies.Add(enemy);
            originalEnemies.Add(enemy.Duplicate());
        }

        public void AddTrap(ITrap trap)
        {
            traps.Add(trap);
            originalTraps.Add(trap.Duplicate());
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

        public void AddCondition(ICondition condition)
        {
            conditions.Add(condition);
        }
        
	    public void AddSpawner(ISpawner spawner)
        {
            spawners.Add(spawner);
        }

        public bool IsVisited()
        {
            return visited;
        }

        public void SetToVisited()
        {
            visited = true;
        }

        public void Reset()
        {
            List<IItem> itemList = new List<IItem>();
            foreach (IItem item in items)
            {
                if (item.IsKept && item.IsResetKept) itemList.Add(item);
            }
            items = itemList;

            foreach (ICondition condition in conditions)
            {
                condition.Reset();
            }

            foreach (ISpawner spawner in spawners)
            {
                spawner.Reset();
            }
            
	        enemies = new List<IEntity>();
            foreach (IEntity enemy in originalEnemies)
            {
                enemies.Add(enemy.Duplicate());
            }

            traps = new List<ITrap>();
            foreach (ITrap trap in originalTraps)
            {
                traps.Add(trap.Duplicate());
            }
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

        public List<ITrap> Traps
        {
            get => traps;
            set => traps = value;
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

        public List<ICondition> Conditions
        {
            get => conditions;
            set => conditions = value;
        }
        
	    public List<ISpawner> Spawners
        {
            get => spawners;
            set => spawners = value;
        }
    }
}
