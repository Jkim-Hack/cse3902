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

        public void ParseDoors(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName doorsName = XName.Get("doors", doc.Root.Name.NamespaceName);

            XElement doors = roomxml.Element(doorsName);
            List<XElement> doorList = doors.Elements("door").ToList();

            foreach (XElement door in doorList)
            {
                XElement typeName = door.Element("type");
                XElement doorPos = door.Element("pos"); //not used for walls
                XElement initState = door.Element("state"); //used only for normal doors
                XElement xLoc = door.Element("xloc"); //not used for normal doors
                XElement yLoc = door.Element("yloc"); //not used for normal doors

                Vector2 xyChange = new Vector2();
                if (typeName.Value.Equals("PortalUp") || typeName.Value.Equals("PortalDown")) xyChange = GetXYChange(door);

                RoomUtilities.DoorPos dPos = FindDoorPos(typeName.Value);
                Vector2 center;
                if (dPos == RoomUtilities.DoorPos.NONE) center = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(Int32.Parse(xLoc.Value), Int32.Parse(yLoc.Value)));
                else center = RoomUtilities.CalculateDoorCenter(roomobj.RoomPos, dPos);

                IDoor.DoorState initialDoorState = GetInitialDoorState(initState.Value);
                IDoor doorAdd = CreateDoor(typeName.Value, center, initialDoorState, xyChange);
                Vector3 connectingRoom = roomobj.RoomPos + GetConnectingRoom(typeName.Value, xyChange);

                if (roomHandler.rooms.ContainsKey(connectingRoom))
                {
                    HandleDoorConnection(connectingRoom, ref doorAdd, Int32.Parse(doorPos.Value));
                }

                roomobj.AddDoor(doorAdd);
            }
        }

        private IDoor CreateDoor(String type, Vector2 startingPos, IDoor.DoorState initialDoorState, Vector2 xyChange)
        {
            IDoor newDoor = null;
            switch (type)
            {
                case "Up":
                    newDoor = new NormalUpDoor(game, startingPos, initialDoorState);
                    break;
                case "Down":
                    newDoor = new NormalDownDoor(game, startingPos, initialDoorState);
                    break;
                case "Left":
                    newDoor = new NormalLeftDoor(game, startingPos, initialDoorState);
                    break;
                case "Right":
                    newDoor = new NormalRightDoor(game, startingPos, initialDoorState);
                    break;
                case "StairDown":
                    newDoor = new DownStaircaseDoor(game, startingPos);
                    break;
                case "OffscreenUp":
                    newDoor = new OffscreenUpDoor(game, startingPos);
                    break;
                case "PortalUp":
                    newDoor = new PortalUp(game, startingPos, xyChange);
                    break;
                case "PortalDown":
                    newDoor = new PortalDown(game, startingPos, xyChange);
                    break;
                default:
                    break;
            }

            return newDoor;
        }

        private Vector2 GetXYChange(XElement door)
        {
            XElement xChange = door.Element("xchange");
            XElement yChange = door.Element("ychange");

            return new Vector2(Int32.Parse(xChange.Value), Int32.Parse(yChange.Value));
        }

        private Vector3 GetConnectingRoom(String type, Vector2 xyChange)
        {
            switch (type)
            {
                case "Up":
                    return new Vector3(0, -1, 0);
                case "Down":
                    return new Vector3(0, 1, 0);
                case "Left":
                    return new Vector3(-1, 0, 0);
                case "Right":
                    return new Vector3(1, 0, 0);
                case "StairDown":
                    return new Vector3(0, 0, -1);
                case "OffscreenUp":
                    return new Vector3(0, 0, 1);
                case "PortalUp":
                    return new Vector3(xyChange, 2);
                case "PortalDown":
                    return new Vector3(xyChange, -2);
                default: //this should never happen
                    return new Vector3(0, -1, 0);
            }
        }

        private IDoor.DoorState GetInitialDoorState(String state)
        {
            switch (state)
            {
                case "Open":
                    return IDoor.DoorState.Open;
                case "Closed":
                    return IDoor.DoorState.Closed;
                case "Locked":
                    return IDoor.DoorState.Locked;
                case "Wall":
                    return IDoor.DoorState.Wall;
                case "Bombed":
                    return IDoor.DoorState.Bombed;
                default:
                    return IDoor.DoorState.Open;
            }
        }

        private RoomUtilities.DoorPos FindDoorPos(String pos)
        {
            RoomUtilities.DoorPos doorPos;
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
                default:
                    doorPos = RoomUtilities.DoorPos.NONE;
                    break;
            }

            return doorPos;
        }

        private void HandleDoorConnection(Vector3 connectingRoom, ref IDoor door, int pos)
        {
            roomHandler.rooms.GetValueOrDefault(connectingRoom).Doors[pos].ConnectedDoor = door;
            door.ConnectedDoor = roomHandler.rooms.GetValueOrDefault(connectingRoom).Doors[pos];
        }
    }
}
