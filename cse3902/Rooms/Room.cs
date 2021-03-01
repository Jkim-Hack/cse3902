using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using cse3902.Entities;

namespace cse3902.Rooms
{
    public class Room
    {
        //private SpriteBatch spriteBatch;

        private Vector2 topLeft;
        private Vector2 bottomRight;
        private bool visited;
        private int[] surrounding;

        private List<IItem> items;
        private List<IEntity> enemies;
        private List<IProjectile> projectiles;

        public Room(int top, int right, int bottom, int left, Vector2 tl, Vector2 br)
        {
            surrounding[0] = top;
            surrounding[1] = right;
            surrounding[2] = bottom;
            surrounding[3] = left;

            topLeft = tl;
            bottomRight = br;

            visited = false;
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
    }
}
