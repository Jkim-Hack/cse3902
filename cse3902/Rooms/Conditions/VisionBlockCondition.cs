using cse3902.Interfaces;
using cse3902.HUD;

namespace cse3902.Rooms.Conditions
{
    public class VisionBlockCondition : ICondition
    {
        public VisionBlockCondition()
        {
        }

        public void CheckCondition()
        {
            VisionBlocker.Instance.VisionIsBlocked = InventoryManager.Instance.ItemSlot != InventoryManager.ItemType.Candle;
        }

        public void SendSignal()
        {

        }

        public void Reset()
        {

        }

        public void EnterRoom()
        {
        }

        public void LeaveRoom()
        {
            VisionBlocker.Instance.VisionIsBlocked = false;
        }
    }
}
