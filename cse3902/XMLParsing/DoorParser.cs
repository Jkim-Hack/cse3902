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
    public class DoorParser
    {
        private Game1 game;
        private RoomHandler roomHandler;

        public DoorParser(Game1 gm, RoomHandler rh)
        {
            game = gm;
            roomHandler = rh;
        }

        public void parseDoors(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName doorsName = XName.Get("doors", doc.Root.Name.NamespaceName);

            XElement doors = roomxml.Element(doorsName);
            List<XElement> doorList = doors.Elements("door").ToList();

            foreach (XElement door in doorList)
            {
                XElement typeName = door.Element("type");
                XElement connRoom = door.Element("connRoom");
                XElement doorPos = door.Element("pos");

                // TODO: update once the Vector3 to Vector2 method is written
                Vector2 center = RoomUtilities.CalculateDoorCenter(roomobj.roomPos, FindDoorPos(typeName.Value));

                IDoor doorAdd = createDoor(typeName.Value, center);

                Vector3 connectingRoom = RoomUtilities.ConvertToVector3(connRoom.Value);

                if (roomHandler.rooms.ContainsKey(connectingRoom))
                {
                    HandleDoorConnection(roomobj.roomPos, connectingRoom, ref doorAdd, Int32.Parse(doorPos.Value));
                }

                roomobj.AddDoor(doorAdd);
            }
        }

        private IDoor createDoor(String type, Vector2 startingPos)
        {
            IDoor newDoor = null;
            switch (type)
            {
                case "Up":
                    newDoor = new NormalUpDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Down":
                    newDoor = new NormalDownDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Left":
                    newDoor = new NormalLeftDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Right":
                    newDoor = new NormalRightDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "StairDown":
                    newDoor = new DownStaircaseDoor(game, startingPos);
                    break;
                case "OffscreenUp":
                    newDoor = new OffscreenUpDoor(game, startingPos);
                    break;
                default:
                    break;
            }

            return newDoor;
        }

        private RoomUtilities.DoorPos FindDoorPos(String pos)
        {
            RoomUtilities.DoorPos doorPos = RoomUtilities.DoorPos.NONE;
            switch (pos)
            {
                case "Up":
                    doorPos = RoomUtilities.DoorPos.TOP;
                    break;
                case "Down":
                    doorPos = RoomUtilities.DoorPos.BOTTOM;
                    break;
                case "Left":
                    doorPos = RoomUtilities.DoorPos.LEFT;
                    break;
                case "Right":
                    doorPos = RoomUtilities.DoorPos.RIGHT;
                    break;
                case "StairDown":
                    doorPos = RoomUtilities.DoorPos.NONE;
                    break;
                case "OffscreenUp":
                    doorPos = RoomUtilities.DoorPos.NONE;
                    break;
                default:
                    break;
            }

            return doorPos;
        }

        private void HandleDoorConnection(Vector3 currRoom, Vector3 connectingRoom, ref IDoor door, int pos)
        {
            roomHandler.rooms.GetValueOrDefault(connectingRoom).Doors[pos].ConnectedDoor = door;
            door.ConnectedDoor = roomHandler.rooms.GetValueOrDefault(connectingRoom).Doors[pos];
        }
    }
}
