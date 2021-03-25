using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomConditions
    {
        public IList conditions;

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
            conditions = new List<RoomConditionList.CheckingCondition>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (RoomConditionList.CheckingCondition condition in conditions)
            {
                condition();
            }
        }

        public void LoadNewRoom(ref List<RoomConditionList.CheckingCondition> oldList, List<RoomConditionList.CheckingCondition> newList)
        {
            oldList = new List<RoomConditionList.CheckingCondition>();

            List<RoomConditionList.CheckingCondition> conditiontemp = conditions as List<RoomConditionList.CheckingCondition>;

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
