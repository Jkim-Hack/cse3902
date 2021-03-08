
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Collision
{
    /* Inspiration from: https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374 */
    public class QuadTree
    {
        private int maxObjectsPerNode = 10;
        private int maxTreeLevel = 10;

        private int currentLevel;
        private List<Rectangle> allObjects;
        private Rectangle nodeBounds;
        private QuadTree[] nodes;

        public QuadTree(int level, Rectangle nodeBounds)
        {
            this.currentLevel = level;
            this.nodeBounds = nodeBounds;
            this.allObjects = new List<Rectangle>();
            this.nodes = new QuadTree[4];
        }

        public void Insert(Rectangle rectangle)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(rectangle);
                if (index != -1)
                {
                    nodes[index].Insert(rectangle);
                    return;
                }
            }

            allObjects.Add(rectangle);

            if (allObjects.Count > maxObjectsPerNode && currentLevel < maxTreeLevel)
            {
                if (nodes[0] == null) Divide();
                int i = 0;
                while (i < allObjects.Count)
                {
                    int index = GetIndex(allObjects[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(allObjects[i]);
                        allObjects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }

        }

        public List<Rectangle> GetLikelyCollidedObjects(List<Rectangle> returnObjects, Rectangle rectangle)
        {
            int index = GetIndex(rectangle);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].GetLikelyCollidedObjects(returnObjects, rectangle);
            }
            returnObjects.AddRange(allObjects);
            return returnObjects;
        }

        public void Clear()
        {
            this.allObjects.Clear();
            for (int i = 0; i < nodes.Length; ++i)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        private int GetIndex(Rectangle rectangle)
        {
            int index = -1;
            float vMidpoint = nodeBounds.X + nodeBounds.Width / 2;
            float hMidpoint = nodeBounds.Y + nodeBounds.Height / 2;

            bool canFitTop = rectangle.Y < hMidpoint && rectangle.Y + rectangle.Height < hMidpoint;
            bool canFitBottom = nodeBounds.Y > hMidpoint;

            bool canFitLeft = rectangle.X < vMidpoint && rectangle.X + rectangle.Width < vMidpoint;
            bool canFitRight = rectangle.X > vMidpoint;

            if (canFitLeft)
            {
                if (canFitTop) index = 1;
                else if (canFitBottom) index = 2;
            }
            else if (canFitRight)
            {
                if (canFitTop) index = 0;
                else if (canFitBottom) index = 3;
            }

            return index;
        }

        private void Divide()
        {
            int subNodeWidth = nodeBounds.Width / 2;
            int subNodeHeight = nodeBounds.Height / 2;

            int x = nodeBounds.X;
            int y = nodeBounds.Y;

            nodes[0] = new QuadTree(currentLevel + 1, new Rectangle(x + subNodeWidth, y, subNodeWidth, subNodeHeight));
            nodes[1] = new QuadTree(currentLevel + 1, new Rectangle(x, y, subNodeWidth, subNodeHeight));
            nodes[2] = new QuadTree(currentLevel + 1, new Rectangle(x, y + subNodeHeight, subNodeWidth, subNodeHeight));
            nodes[3] = new QuadTree(currentLevel + 1, new Rectangle(x + subNodeWidth, y + subNodeHeight, subNodeWidth, subNodeHeight));
        }
    }
}