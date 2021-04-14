using System.Collections.Generic;
using System;
using System.Xml.Linq;
using cse3902.Rooms;
using System.Linq;
using cse3902.Interfaces;
using cse3902.Rooms.Conditions;
using Microsoft.Xna.Framework;
namespace cse3902.XMLParsing
{
    public class ConditionParser
    {
        public ConditionParser()
        {
        }

        public void ParseCondtions(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName conditionsName = XName.Get("conditions", doc.Root.Name.NamespaceName);

            XElement conditions = roomxml.Element(conditionsName);
            List<XElement> conditionList = conditions.Elements("condition").ToList();

            foreach (XElement condition in conditionList)
            {
                XElement conditionID = condition.Element("id");
                XElement variables = condition.Element("variables");
                List<XElement> variableList = variables.Elements("variable").ToList();

                roomobj.AddCondition(CreateCondition(conditionID.Value,GetConditionVariables(variableList),roomobj));
            }
        }

        private ICondition CreateCondition(string id, List<int> conditionVariables, Room roomobj)
        {
            ICondition condition = null;
            switch (id)
            {
                case "0":
                    Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(conditionVariables[0], conditionVariables[1]));
                    conditionVariables[0] = (int)truePos.X;
                    conditionVariables[1] = (int)truePos.Y;
                    condition = new AllEnemiesKilledDropItem(conditionVariables);
                    break;
                case "1":
                    condition = new AllEnemiesKilledOpenDoor(conditionVariables);
                    break;
                case "2":
                    condition = new AllBlocksPushedOpenDoor(conditionVariables);
                    break;
                case "3":
                    condition = new AllEnemiesKilledBlocksPushable();
                    break;
                case "4":
                    condition = new AllBlocksPushedPlaySound();
                    break;
                case "5":
                    condition = new RickRollRoom();
                    break;
                case "6":
                    condition = new VisionBlockCondition();
                    break;
                default: //this should never happen
                    break;
            }

            return condition;
        }

        private List<int> GetConditionVariables(List<XElement> variableList)
        {
            List<int> list = new List<int>();
            
            foreach (XElement variable in variableList)
            {
                list.Add(Int32.Parse(variable.Value));
            }

            return list;
        }
    }
}
