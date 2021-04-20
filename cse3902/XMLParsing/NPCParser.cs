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
                XElement text = npc.Element("text");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                INPC npcAdd = CreateNPC(typeName.Value, truePos, text.Value);
                roomobj.AddNPC(npcAdd);
            }
        }

        private INPC CreateNPC(String type, Vector2 startingPos, String text)
        {
            INPC newEnemy = new OldManNPC(game, startingPos, text);
            switch (type)
            {
                case "OldMan":
                    newEnemy = new OldManNPC(game, startingPos, text);
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
                case "RickRoll":
                    newEnemy = new RickRollNPC(game, startingPos);
                    break;
                default:
                    break;
            }
            return newEnemy;
        }

    }
}
