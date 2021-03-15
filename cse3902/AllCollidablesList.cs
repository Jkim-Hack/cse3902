using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cse3902.Collision;
using Microsoft.Xna.Framework;

namespace cse3902
{
    public class AllCollidablesList
    {
        private Dictionary<int, IList> AllColldiables;

        public AllCollidablesList()
        {
            AllColldiables = new Dictionary<int, IList>();
        }

        public IList GetList(int priority)
        {
            return AllColldiables[priority];
        }

        // False means, you need to call InsertNewList first
        public void Insert(int priority, ICollidableItemEntity item)
        {
            if (AllColldiables.ContainsKey(priority))
            {
                AllColldiables[priority].Add(item);
            }
            else
            {
                AllColldiables.Add(priority, new List<ICollidableItemEntity>());
                Insert(priority, item);
            }
        }

        public void InsertNewList(int priority, ref IList collidablesList)
        {
            AllColldiables[priority] = collidablesList;
        }

        public Dictionary<int, Dictionary<Rectangle, ICollidable>> GetAllCollidablesDictionary()
        {
            Dictionary<int, Dictionary<Rectangle, ICollidable>> allCollidablesDictionary = new Dictionary<int, Dictionary<Rectangle, ICollidable>>();
            foreach (var priority in AllColldiables.Keys)
            {
                allCollidablesDictionary.Add(priority, new Dictionary<Rectangle, ICollidable>());
                if (AllColldiables[priority] != null)
                {
		            foreach (var collidable in AllColldiables[priority].Cast<ICollidableItemEntity>().ToList())
                    {
                        if (!allCollidablesDictionary[priority].ContainsKey(collidable.Collidable.RectangleRef))
                        {
                            allCollidablesDictionary[priority].Add(collidable.Collidable.RectangleRef, collidable.Collidable);
                        }
                    }
                }
            }
            return allCollidablesDictionary;
        }
    }
}
