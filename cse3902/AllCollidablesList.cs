using System;
using System.Collections;
using System.Collections.Generic;
using cse3902.Collision;
using Microsoft.Xna.Framework;

namespace cse3902
{
    public class AllCollidablesList
    {
        private Dictionary<int, Dictionary<Type, IList>> AllColldiables;

        public AllCollidablesList()
        {
            AllColldiables = new Dictionary<int, Dictionary<Type, IList>>();
        }

        public IList GetList(int priority, Type listType)
        {
            return AllColldiables[priority][listType];
        }

        // False means, you need to call InsertNewList first
        public bool Insert(Object item, int priority)
        {
            if (AllColldiables.ContainsKey(priority) && AllColldiables[priority].ContainsKey(item.GetType()))
            {
                AllColldiables[priority][item.GetType()].Add(item);
                return true;
            }
            return false;
        }

        public void InsertNewList(IList collidablesList, Type collidableType, int priority)
        {
            if (AllColldiables.ContainsKey(priority))
            { 
                AllColldiables[priority].Add(collidableType, collidablesList);
            }
            else
            {
                AllColldiables.Add(priority, new Dictionary<Type, IList>());
                // Lazy man's way ;)
                InsertNewList(collidablesList, collidableType, priority);
            }
        }

        public Dictionary<int, Dictionary<Rectangle, ICollidable>> GetAllCollidablesDictionary()
        {
            Dictionary<int, Dictionary<Rectangle, ICollidable>> allCollidablesDictionary = new Dictionary<int, Dictionary<Rectangle, ICollidable>>();
            foreach (var priority in Priorities)
            {
                allCollidablesDictionary.Add(priority, new Dictionary<Rectangle, ICollidable>());
                foreach (var type in ListKeys(priority))
                {
                    foreach (var item in AllColldiables[priority][type]) 
		            {
                        dynamic collidable = Convert.ChangeType(item, type);
                        if (type != typeof(ICollidable))
                        {
                            allCollidablesDictionary[priority].Add(collidable.Collidable.RectangleRef, collidable.Collidable);
                        }
                        else if (type == typeof(ICollidable))
                        {
                            allCollidablesDictionary[priority].Add(collidable.RectangleRef, collidable);
                        }
                    }
                }
            }
            return allCollidablesDictionary;
        }

        public Dictionary<Type, IList>.KeyCollection ListKeys(int priority)
        {
            return AllColldiables[priority].Keys;
        }
         
	    public Dictionary<Type, IList>.ValueCollection ListValues(int priority)
        {
            return AllColldiables[priority].Values;
        }

	    public Dictionary<int, Dictionary<Type, IList>>.KeyCollection Priorities
        {
            get => AllColldiables.Keys;
        }
    }
}
