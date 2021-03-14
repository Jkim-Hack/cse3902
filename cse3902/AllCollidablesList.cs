using System;
using System.Collections;
using System.Collections.Generic;

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

        public Dictionary<Type, IList>.KeyCollection ListKeys(int priority)
        {
            return AllColldiables[priority].Keys;
        }
         
	    public Dictionary<Type, IList>.ValueCollection ListValues(int priority)
        {
            return AllColldiables[priority].Values;
        }

	    public Dictionary<int, Dictionary<Type, IList>>.KeyCollection Priorities()
        {
            return AllColldiables.Keys;
        }
    }
}
