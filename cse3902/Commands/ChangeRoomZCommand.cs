using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Doors;
using cse3902.Constants;

namespace cse3902.Commands
{
    public class ChangeRoomZCommand : ICommand
    {
        private Game1 game;

        public ChangeRoomZCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            if (!GameStateManager.Instance.IsUnpaused()) return;

            id = id % CommandConstants.ChangeRoomZCommandCount;
            Vector3 direction;
            Vector3 currentRoom = game.RoomHandler.currentRoom;
            int ans;
            switch (id)
            {
                case 0:
                    direction = new Vector3(0, 0, 1);
                    if (currentRoom.Z >= 0) direction = GetXYChange(currentRoom, true);
                    ans = 4;
                    break;
                case 1:
                    direction = new Vector3(0, 0, -1);
                    ans = 0;
                    if (currentRoom.Z > 0)
                    {
                        direction = GetXYChange(currentRoom, false);
                        ans = 4;
                    }
                    break;
                default: //this should never happen
                    direction = new Vector3(0, 0, 1);
                    if (currentRoom.Z >= 0) GetXYChange(currentRoom, true);
                    ans = 4;
                    break;
            }

            game.RoomHandler.LoadNewRoom(direction,ans);
        }

        public void Unexecute()
        {

        }

        private Vector3 GetXYChange(Vector3 currentRoom, bool isUp)
        {
            Vector3 change = CommandConstants.InvalidChange;
            if (game.RoomHandler.rooms[currentRoom].Doors.Count > CommandConstants.NormalDoorCount)
            {
                IDoor door = game.RoomHandler.rooms[currentRoom].Doors[CommandConstants.NormalDoorCount];
                if (door is PortalDown && !isUp) change = ((PortalDown)door).RoomTranslationVector;
                else if (door is PortalUp && isUp) change = ((PortalUp)door).RoomTranslationVector;
            }
            return change;
        }
    }
}