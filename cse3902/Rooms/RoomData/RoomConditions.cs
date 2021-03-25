using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Collections;
using cse3902.Interfaces;

namespace cse3902.Rooms
{
    public class RoomConditions
    {
        private IList conditions;

        private static RoomConditions instance = new RoomConditions();

        public static RoomConditions Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomConditions()
        {
            conditions = new List<ICondition>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (ICondition condition in conditions)
            {
                condition.CheckCondition();
            }
        }

        public void LoadNewRoom(ref List<ICondition> oldList, List<ICondition> newList)
        {
            oldList = new List<ICondition>();

            List<ICondition> conditiontemp = conditions as List<ICondition>;

            for (int i = 0; i < conditions.Count; i++)
            {
                oldList.Add(conditiontemp[i]);
            }

            conditions.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                conditions.Add(newList[i]);
            }
        }
    }
}
