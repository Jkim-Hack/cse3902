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
                    List<ICollidable> collided = GetCollided(collidable.Key);
                    /* call appropriate methods */
                }
            }
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

        /* Finds all ICollidable objects that the given rectangle has collided with */
        private List<ICollidable> GetCollided(Rectangle reference)
        {
            List<ICollidable> collided = new List<ICollidable>();

            var likelyCollided = quadTree.GetLikelyCollidedObjects(new List<Rectangle>(), reference);
            foreach (var rectangle in likelyCollided)
            {
                if (reference.Intersects(rectangle))
                {
                    collided.Add(FindCollided(rectangle));
                }
            }

            return collided;
        }

        /* Finds ICollidable object associated with given rectangle */
        private ICollidable FindCollided(Rectangle collided)
        {
            foreach (int priority in allCollidableObjects.Keys)
            {
                if (allCollidableObjects[priority].ContainsKey(collided)) 
                {
                    return allCollidableObjects[priority][collided];
                }
            }

            return null;
        }
    }
}
