using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Rooms.Conditions;

namespace cse3902
{
    public class GameConditionManager
    {
        List<ICondition> conditions;
        Game1 game;

        private static GameConditionManager gameConditionManagerInstance = new GameConditionManager();
        public static GameConditionManager Instance
        {
            get => gameConditionManagerInstance;
        }
        private GameConditionManager()
        {
        }
        public void Update()
        {
            foreach (ICondition condition in conditions)
            {
                condition.CheckCondition();
            }
        }

        public void Reset()
        {
            foreach (ICondition condition in conditions)
            {
                condition.Reset();
            }
        }

        public Game1 Game
        {
            set
            {
                game = value;

                conditions = new List<ICondition>()
                {
                    new HealthChangeCondition(game),
                };
            }
        }
    }
}