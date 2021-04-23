using System;
using cse3902.Rooms;

namespace cse3902.Constants
{
    public class MovementConstants
    {
        public const int StartingTravelDistance = 20;
        public const int StartingShoveDistance = -10;

        public const int DefaultShoveDistance = 20;

        public const float AquamentusSpeed = 10.0f;
        public const float AquamentusDelay = .2f;
        public const int AquamentusMaxTravel = 150;
        public const int AquamentusShoveDistance = DefaultShoveDistance;
        public const int AquamentusFireballChangeX = 15;
        public static readonly double AquamentusFireballSpreadAngle = Math.Atan2(1, 3);

        public const float DodongoSpeed = 25.0f;
        public const float DodongoDelay = .2f;
        public const int DodongoMaxTravel = 100;
        public const int DodongoShoveDistance = 3*DefaultShoveDistance;
        public const float DodongoSizeIncrease = 0.7f;

        public const float BoggusSpeed = 25.0f;
        public const float BoggusDelay = .2f;
        public const int BoggusMaxTravel = 75;
        public const int BoggusShoveDistance = DefaultShoveDistance;
        public static readonly double BoggusFireballSpreadAngle = Math.Atan2(1, 2);


        public const float MarioSpeed = 45.0f;
        public const float MarioDelay = .2f;
        public const int MarioMaxTravel = 100;
        public const int MarioShoveDistance = DefaultShoveDistance;

        public const float GelSpeed = 25.0f;
        public const float GelDelay = .2f;
        public const int GelMaxTravel = 100;
        public const int GelShoveDistance = DefaultShoveDistance;

        public const float GoriyaSpeed = 50.0f;
        public const float GoriyaDelay = .2f;
        public const int GoriyaMaxTravel = 100;
        public const int GoriyaShoveDistance = DefaultShoveDistance;

        public const float KeeseSpeedNormal = 30.0f;
        public const float KeeseDelay = .2f;
        public const float KeeseSpeedSlow = 21.0f;
        public const int KeeseMaxTravel = 100;
        public const int KeeseShoveDistance = DefaultShoveDistance;

        public const float StalfosSpeed = 30.0f;
        public const float StalfosDelay = .2f;
        public const int StalfosMaxTravel = 80;
        public const int StalfosShoveDistance = DefaultShoveDistance;

        public const float TrapSpeed = 50f;
        public const int TrapTriggerDistanceX = 100;
        public const int TrapTriggerDistanceY = 50;
        public const int TrapTime = 20;

        public const float WallMasterSpeed = 45.0f;
        public const int WallMasterMaxTravel = (int)((RoomUtilities.BLOCK_SIDE*4)*(30/WallMasterSpeed));
        public const int WallMasterShoveDistance = DefaultShoveDistance;

        public const float RopeSpeed = 30.0f;
        public const float RopeDelay = .2f;
        public const int RopeMaxTravel = 100;
        public const int RopeShoveDistance = DefaultShoveDistance;

        public const float ZolSpeed = 40.0f;
        public const int ZolMaxTravel = 120;
        public const int ZolShoveDistance = DefaultShoveDistance;

        public const int BlockPushThreshold = 20;
        public const float BlockPushSpeed = .5f;

        public const int NormalDoorRelease = 50;
        public const int OffscreenRelease = 40;
        public const int PortalRelease = 20;

        /*
        public const float Speed = 25.0f;
        public const int MaxTravel = 125;
        public const int ShoveDistance = DefaultShoveDistance;
        */

    }
}
