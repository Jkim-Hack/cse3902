using cse3902.HUD;
using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkUseItemCommand : ICommand
    {
        private Game1 game;

        public LinkUseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 4;
            id++;

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning()) 
            {
                if (id == 1)
                {
                    game.Player.Attack();
                }
                else
                {
                    if (id == 2)
                    {
                        game.Player.ChangeItem((int)InventoryManager.ItemType.Bow);
                    }
                    if (id == 3)
                    {
                        game.Player.ChangeItem((int)InventoryManager.ItemType.Boomerang);
                    }
                    if (id == 4)
                    {
                        game.Player.ChangeItem((int)InventoryManager.ItemType.Bomb);
                    }
                    game.Player.UseItem();
                }
            }
        }

        public void Unexecute()
        {

        }
    }
}