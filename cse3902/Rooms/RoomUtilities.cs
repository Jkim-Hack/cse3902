using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace cse3902.Rooms
{
    class RoomUtilities
    {
        public const int ROOM_WIDTH = 256;
        public const int ROOM_HEIGHT = 176;
        public const int NUM_ROOMS_X = 6;
        public const int NUM_ROOMS_Y = 6;

        public const int CAMERA_CYCLES = 30;

        public const int NUM_BLOCKS_X = 12;
        public const int NUM_BLOCKS_Y = 7;
        public const int INTERIOR_WIDTH = 192;
        public const int INTERIOR_HEIGHT = 112;

        public const int WALL_SIZE = (ROOM_WIDTH - INTERIOR_WIDTH) / 2;

        public const int INTERIOR_TEXTURE_ROWS = 5;
        public const int INTERIOR_TEXTURE_COLS = 3;

        public const int NUM_OF_WALLS = 8;
        public const int DOOR_START_X = 111;
        public const int DOOR_START_Y = 71;
        public const int DOOR_END_X = DOOR_START_X+32;
        public const int DOOR_END_Y = DOOR_START_Y+32;

        public const int WALL_WIDTH = 120;
        public const int WALL_HEIGHT = 80;

        public enum DoorPos
        {
            Up,
            Down,
            Left,
            Right
        }

        public static Vector2 calculateRoomCenter(Vector2 roomLoc)
        {
            roomLoc *= new Vector2(ROOM_WIDTH, ROOM_HEIGHT);
            roomLoc += new Vector2(ROOM_WIDTH/2, ROOM_HEIGHT/2);
            return roomLoc;
        }

        public static Vector2 calculateBlockCenter(Vector2 roomLoc, Vector2 blockLoc)
        {
            roomLoc *= new Vector2(ROOM_WIDTH, ROOM_HEIGHT);
            roomLoc += new Vector2(WALL_SIZE, WALL_SIZE);
            blockLoc *= new Vector2(INTERIOR_WIDTH / NUM_BLOCKS_X, INTERIOR_HEIGHT/ NUM_BLOCKS_Y);
            blockLoc += new Vector2((INTERIOR_WIDTH / NUM_BLOCKS_X)/2, (INTERIOR_HEIGHT / NUM_BLOCKS_Y)/2);
            roomLoc += blockLoc;
            return roomLoc;
        }

        public static Vector2 calculateDoorCenter(Vector2 roomLoc, DoorPos pos)
        {
            return new Vector2(0, 0);
        }

        public static Rectangle[] getWallRectangles(Vector2 roomLoc)
        {
            roomLoc *= new Vector2(ROOM_WIDTH, ROOM_HEIGHT);
            Rectangle[] rectangles = new Rectangle[NUM_OF_WALLS];
            int current = 0;

            rectangles[current++] = new Rectangle((int)roomLoc.X, (int)roomLoc.Y, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)roomLoc.X, (int)roomLoc.Y, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)roomLoc.X + ROOM_WIDTH - WALL_WIDTH, (int)roomLoc.Y, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)roomLoc.X, (int)roomLoc.Y + ROOM_HEIGHT - WALL_HEIGHT, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)roomLoc.X + ROOM_WIDTH - WALL_SIZE, (int)roomLoc.Y, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)roomLoc.X, (int)roomLoc.Y + ROOM_HEIGHT - WALL_SIZE, WALL_WIDTH, WALL_SIZE);
            rectangles[current++] = new Rectangle((int)roomLoc.X + ROOM_WIDTH - WALL_SIZE, (int)roomLoc.Y + ROOM_HEIGHT - WALL_HEIGHT, WALL_SIZE, WALL_HEIGHT);
            rectangles[current++] = new Rectangle((int)roomLoc.X + ROOM_WIDTH - WALL_WIDTH, (int)roomLoc.Y + ROOM_HEIGHT - WALL_SIZE, WALL_WIDTH, WALL_SIZE);
            return rectangles;
        }
    }
}
