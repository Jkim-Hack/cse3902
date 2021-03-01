using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Collision
{
    public class CollisionManager
    {
        // priority levels are used based on which collidable object's OnCollideWith method executes first
        // Lower integer = higher priority

        // <priority level, <Collidable's Rectangle, Collidable object>>
        private Dictionary<int, Dictionary<Rectangle, ICollidable>> allCollidableObjects;
        private QuadTree quadTree;

        public CollisionManager(int width, int height)
        {
            quadTree = new QuadTree(0, new Rectangle(0, 0, width, height));
            allCollidableObjects = new Dictionary<int, Dictionary<Rectangle, ICollidable>>();
        }

        public void InsertCollidable(int priority, ICollidable collidable)
        {
            if (allCollidableObjects.ContainsKey(priority))
            {
                allCollidableObjects[priority].Add(collidable.RectangleRef, collidable);
            }
            else
            {
                allCollidableObjects.Add(priority, new Dictionary<Rectangle, ICollidable>());
                // The lazy man's way ;)
                InsertCollidable(priority, collidable);
            }
        }

        public void Update()
        {
            quadTree.Clear();
            foreach (var collidable in allCollidableObjects)
            {
                foreach (var rectangle in collidable.Value.Keys)
                {
                    quadTree.Insert(rectangle);
                }
            }
        }

    }
}