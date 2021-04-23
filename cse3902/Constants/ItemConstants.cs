using System;
using cse3902.Rooms;

namespace cse3902.Constants
{
    class ItemConstants
    {
        public const float Angle90Rad = Angle180Rad / 2.0f;
        public const float Angle180Rad = (float) Math.PI;
        public const float Angle270Rad = 3 * Angle90Rad;
        public const float AnglePiOver8 = Angle180Rad / 8f;
        public const float Angle0Rad = 0;
        public const float epsilon = 0.1f;

        public const int SWORDROWS = 2;
        public const int SWORDCOLS = 2;

        public const int CloudRows = 4;
        public const int CloudCols = 3;
        public const float CloudDelay = 0.05f;

        public const int FairyRows = 1;
        public const int FairyCols = 2;
        public const float FairyDelay = 0.2f;

        public const int HeartRows = 1;
        public const int HeartCols = 2;
        public const float HeartDelay = 0.2f;

        public const int RupeeRows = 2;
        public const int RupeeCols = 1;
        public const float RupeeDelay = 0.2f;

        public const int TriforceRows = 2;
        public const int TriforceCols = 1;
        public const float TriforceDelay = 0.2f;

        public const int ArrowSpeed = 2;
        public const int ArrowCollisionFrames = 5;

        public const int BombRows = 2;
        public const int BombCols = 1;
        public const float BombDelay = 0.8f;

        public const int BoomerangTravelDistance = (int) (5 * RoomUtilities.BLOCK_SIDE / BoomerangSpeed);
        public const int EnemyBoomerangTravelDistance = (int)(6 * RoomUtilities.BLOCK_SIDE / BoomerangSpeed);
        public const float BoomerangSpeed = 2f;

        public const float FireballSpeed = 1.3f;
        public const float MagicFireballSpeed = 1.5f;
        public const float FireballDelay = 3f;

        public const float PoofDelay = .05f;
        public const int PoofRows = 3;
        public const int PoofCols = 2;
        public const int PoofOffsetX = 25;
        public const int PoofOffsetY = 75;

        public const int SwordBeamRows = 2;
        public const int SwordBeamCols = 2;
        public const int SwordBeamSpeed = 2;
        public const float SwordBeamDelay= .2f;

        public const int MagicBeamRows = 2;
        public const int MagicBeamCols = 2;
        public const int MagicBeamSpeed = 2;
        public const float MagicBeamDelay = .2f;
        public const int MagicFireballCollision = 3;

    }
}
