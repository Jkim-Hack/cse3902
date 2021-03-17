using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.NPCs;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class NPCParser
    {
        private Game1 game;

        public NPCParser(Game1 gm)
        {
            game = gm;
        }

        public void ParseNPCs(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName npcsName = XName.Get("npcs", doc.Root.Name.NamespaceName);

            XElement npcs = roomxml.Element(npcsName);
            List<XElement> npcList = npcs.Elements("npc").ToList();

            foreach (XElement npc in npcList)
            {
                XElement typeName = npc.Element("type");
                XElement xloc = npc.Element("xloc");
                XElement yloc = npc.Element("yloc");

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                INPC npcAdd = CreateNPC(typeName.Value, truePos);
                roomobj.AddNPC(npcAdd);
            }
        }

        private INPC CreateNPC(String type, Vector2 startingPos)
        {
            INPC newEnemy = new OldManNPC(game, startingPos);
            switch (type)
            {
                case "OldMan":
                    newEnemy = new OldManNPC(game, startingPos);
                    break;
                case "MedicineWoman":
                    newEnemy = new MedicineWomanNPC(game, startingPos);
                    break;
                case "Merchant":
                    newEnemy = new MerchantNPC(game, startingPos);
                    break;
                case "Flame":
                    newEnemy = new FlameNPC(game, startingPos);
                    break;
                default:
                    break;
            }
            return newEnemy;
        }

    }
}
