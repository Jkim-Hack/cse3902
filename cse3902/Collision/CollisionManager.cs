using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Collision
{
    public class CollisionManager
    {
        // priority levels are used based on which collidable object's OnCollideWith method executes first
        // Lower integer = higher priority

        public enum CollisionPriority
        {
            PLAYER = 0,
            ENEMY_NPC = 1,
            ITEMS = 2,
            BLOCKS = 3,
            BACKGROUND = 4
        }

        // <priority level, <Collidable's Rectangle, Collidable object>>
        private Dictionary<int, Dictionary<Rectangle, ICollidable>> allCollidableObjects;
        private QuadTree quadTree;
        private bool isDisabled;

        private Game1 game;

        public CollisionManager(Game1 game)
        {
            this.game = game;
            isDisabled = false;
            quadTree = new QuadTree(0, new Rectangle(0, 0, this.game.Window.ClientBounds.Width, this.game.Window.ClientBounds.Height));
            allCollidableObjects = new Dictionary<int, Dictionary<Rectangle, ICollidable>>();
        }

        // Thank god there arent hundreds or thousands of collidables in the game
        public void Update()
        {
            if (!isDisabled)
            {
                allCollidableObjects = game.AllCollidablesList.GetAllCollidablesDictionary();
                ResetTree();

                // Check for collisions in the order of priority
                List<int> priorities = new List<int>(allCollidableObjects.Keys);
                priorities.Sort();

                foreach (int priority in priorities)
                {
                    foreach (var collidable in allCollidableObjects[priority])
                    {
                        List<ICollidable> collided = GetCollided(collidable.Key);
                        foreach(var collision in collided)
                        {
                            collidable.Value.OnCollidedWith(collision);
                        }
                    }
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

        public void DrawAllRectangles(Texture2D lineTexture, Color color, int lineWidth)
        {
            SpriteBatch spriteBatch = this.game.spriteBatch;
            foreach (var collidableDictionary in allCollidableObjects.Values)
            {
                foreach (var rectangle in collidableDictionary.Keys)
                {
                    spriteBatch.Draw(lineTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
                    spriteBatch.Draw(lineTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
                    spriteBatch.Draw(lineTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
                    spriteBatch.Draw(lineTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
                }
            }
        }

        public bool Disabled
        {
            get => isDisabled;
            set => isDisabled = value;
        }

    }
}
