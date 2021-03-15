using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace cse3902.Rooms
{
    class RoomUtilities
    {
        public const int BLOCK_SIDE = 16;

        public const int ROOM_WIDTH = BLOCK_SIDE * 16;
        public const int ROOM_HEIGHT = BLOCK_SIDE * 11;
        public const int NUM_ROOMS_X = 6;
        public const int NUM_ROOMS_Y = 6;

        public const int CAMERA_CYCLES = 30;

        public const int NUM_BLOCKS_X = 12;
        public const int NUM_BLOCKS_Y = 7;
        public const int INTERIOR_WIDTH = BLOCK_SIDE * NUM_BLOCKS_X;
        public const int INTERIOR_HEIGHT = BLOCK_SIDE * NUM_BLOCKS_Y;

        public const int WALL_SIZE = (ROOM_WIDTH - INTERIOR_WIDTH) / 2;

        public const int INTERIOR_TEXTURE_ROWS = 5;
        public const int INTERIOR_TEXTURE_COLS = 3;

        public const int NUM_OF_WALLS = 8;
        public const int DOOR_START_X = 111;
        public const int DOOR_START_Y = 71;
        public const int DOOR_END_X = DOOR_START_X + BLOCK_SIDE * 2;
        public const int DOOR_END_Y = DOOR_START_Y + BLOCK_SIDE * 2;

        public const int WALL_WIDTH = 120;
        public const int WALL_HEIGHT = 80;

        public enum DoorPos
        {
            TOP,
            RIGHT,
            BOTTOM,
            LEFT,
            NONE

        }

        public static Vector2 ConvertVector(Vector3 vector)
        {
            return new Vector2(ROOM_WIDTH * (vector.X + NUM_ROOMS_X * vector.Z), vector.Y * ROOM_HEIGHT);
        }

        public static Vector2 CalculateRoomCenter(Vector3 roomLoc)
        {
            Vector2 pos = ConvertVector(roomLoc);
            pos += new Vector2(ROOM_WIDTH / 2, ROOM_HEIGHT / 2);
            return pos;
        }

        public static Vector2 CalculateBlockCenter(Vector3 roomLoc, Vector2 blockLoc)
        {
            Vector2 pos = ConvertVector(roomLoc);
            pos += new Vector2(WALL_SIZE, WALL_SIZE);
            pos *= new Vector2(INTERIOR_WIDTH / NUM_BLOCKS_X, INTERIOR_HEIGHT / NUM_BLOCKS_Y);
            pos += new Vector2((INTERIOR_WIDTH / NUM_BLOCKS_X) / 2, (INTERIOR_HEIGHT / NUM_BLOCKS_Y) / 2);
            pos += blockLoc;
            return pos;
        }

        public static Rectangle[] GetWallRectangles(Vector3 roomLoc)
        {
            Vector2 pos = ConvertVector(roomLoc);
            Rectangle[] rectangles = new Rectangle[NUM_OF_WALLS];
            int current = 0;

            rectangles[current++] = new Rectangle((int)pos.X, (int)pos.Y, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)pos.X, (int)pos.Y, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)pos.X + ROOM_WIDTH - WALL_WIDTH, (int)pos.Y, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)pos.X, (int)pos.Y + ROOM_HEIGHT - WALL_HEIGHT, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)pos.X + ROOM_WIDTH - WALL_SIZE, (int)pos.Y, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)pos.X, (int)pos.Y + ROOM_HEIGHT - WALL_SIZE, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)pos.X + ROOM_WIDTH - WALL_SIZE, (int)pos.Y + ROOM_HEIGHT - WALL_HEIGHT, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)pos.X + ROOM_WIDTH - WALL_WIDTH, (int)pos.Y + ROOM_HEIGHT - WALL_SIZE, WALL_WIDTH, WALL_SIZE);
            return rectangles;
        }

        public static Vector3 ConvertToVector3(String str)
        {
            int comma = str.IndexOf(',');
            int comma2 = str.IndexOf(',', comma + 1);

            Vector3 roomTup = new Vector3(Int32.Parse(str.Substring(0, comma)), Int32.Parse(str.Substring(comma + 1, 1)), Int32.Parse(str.Substring(comma2 + 1)));

            return roomTup;
        }

        public static Vector2 CalculateDoorCenter(Vector3 roomLoc, DoorPos position)
        {
            Vector2 pos = ConvertVector(roomLoc);
            switch (position)
            {
                case DoorPos.TOP:
                    pos += new Vector2(ROOM_WIDTH / 2, WALL_SIZE / 2);
                    break;
                case DoorPos.BOTTOM:
                    pos += new Vector2(ROOM_WIDTH / 2, ROOM_HEIGHT - WALL_SIZE / 2);
                    break;
                case DoorPos.LEFT:
                    pos += new Vector2(WALL_SIZE / 2, ROOM_HEIGHT / 2);
                    break;
                case DoorPos.RIGHT:
                    pos += new Vector2(ROOM_WIDTH - WALL_SIZE / 2, ROOM_HEIGHT / 2);
                    break;
            }
            return pos;
        }
    }
}
