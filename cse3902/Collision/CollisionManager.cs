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
            ResetTree();

            // Check for collisions in the order of priority
            List<int> priorities = new List<int>(allCollidableObjects.Keys);
            priorities.Sort();

            foreach (int priority in priorities)
            {
                foreach (var collidable in allCollidableObjects[priority])
                {
                    
                }
            }
        }

        private List<ICollidable> getCollided(List<int> priorities, Rectangle reference)
        {
            var collided = new List<ICollidable>();

            var likelyCollided = quadTree.GetLikelyCollidedObjects(new List<Rectangle>(), reference);
            foreach(var rectangle in likelyCollided)
            {
                if (reference.Intersects(rectangle))
                {
                    collided.Add(FindCollided());
                }
            }


        }

        private ICollidable FindCollided(List<int> priorities, Rectangle collided)
        {
            ICollidable collidedObject = null;

            foreach (int priority in priorities)
            {
                collidedObject = allCollidableObjects[pr]
                foreach(var collidable in allCollidableObjects[priority])
                {
                    if (collided.Equals(collidable.Key))
                    {
                        return collidable.Value;
                    }
                }
            }

            return collidedObject;
        }

        private void ResetTree()
        {
            quadTree.Clear();
            foreach (var category in allCollidableObjects)
            {
                foreach (var rectangle in category.Value.Keys)
                {
                    quadTree.Insert(rectangle);
                }
            }
        }



    }
}
