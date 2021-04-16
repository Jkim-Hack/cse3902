using cse3902.HUD;
using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class BuyArrowsCommand : ICommand
    {
        private Game1 game;
        bool pressed;

        public BuyArrowsCommand(Game1 game)
        {
            this.game = game;
            pressed = false;
        }

        public void Execute(int id)
        {
            id = id % 3;

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning() && !pressed)
            {
                pressed = true;
                int rupees = InventoryManager.Instance.ItemCount(InventoryManager.ItemType.Rupee);
                int removeRupees = 0;
                int addArrows = 0;

                if (rupees >= 3 * id + 1)
                {
                    removeRupees = 3 * id + 1;
                    addArrows = 5 * id;
                    if (id == 0) addArrows++;
                }

                for (int i = 0; i < removeRupees; i++) InventoryManager.Instance.RemoveFromInventory(InventoryManager.ItemType.Rupee);
                for (int i = 0; i < addArrows; i++) InventoryManager.Instance.AddToInventory(InventoryManager.ItemType.Arrow);
            }
        }

        public void Unexecute()
        {
            pressed = false;
        }
    }
}