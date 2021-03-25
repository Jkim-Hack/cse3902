using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Doors;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class ConditionParser
    {
        private RoomHandler roomHandler;

        public ConditionParser(RoomHandler rh)
        {
            roomHandler = rh;
        }

        public void ParseCondtions(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName conditionsName = XName.Get("conditions", doc.Root.Name.NamespaceName);

            XElement conditions = roomxml.Element(conditionsName);
            List<XElement> conditionList = conditions.Elements("condition").ToList();

            foreach (XElement condition in conditionList)
            {
                XElement conditionID = condition.Element("id");
                roomobj.AddCondition(roomHandler.roomConditionList.GetRoomCondition(Int32.Parse(conditionID.Value)));
            }
        }
    }
}
