using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class CommandConstants
    {
        public const int Cooldown = 15;

        public const int ChangeHealthCommandCount = 4;
        public const int ChangeRoomXYCommandCount = 4;
        public const int ChangeRoomZCommandCount = 2;
        public const int LinkAttackItemCommandCount = 2;
        public const int LinkMovementCommandCount = 4;
        public const int MoveCameraCommandCount = 4;
        public const int UpdateSettingCommandCount = 6;
        public const int PauseCommandCount = 2;

        public static readonly Vector3 InvalidChange = new Vector3(-99, -99, -99);
        public const int NormalDoorCount = 4;

        public const int MoveCameraDistance = 3;
    }
}
