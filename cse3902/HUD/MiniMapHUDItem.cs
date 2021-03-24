using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using System;

namespace cse3902.HUD
{
    public class MiniMapHUDItem
    {

        private Game1 game;
        private Rectangle currentRoom;

        public MiniMapHUDItem(Game1 game)
        {
            this.game = game;
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            foreach(Rectangle rec in MiniMapRoomLayout.RoomLayout)
            {
                Console.WriteLine(rec);
            }
        }
    }
}
