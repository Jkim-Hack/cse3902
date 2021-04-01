using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class OrangeMapHUDItem : IHUDItem
    {
        private Game1 game;
        private Vector3 currentRoom;

        private Texture2D map;
        private Rectangle mapPos;
        
        private int offsetX;
        private int offsetY;

        private bool alreadyChanged;

        public OrangeMapHUDItem()
        {
            
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public void Draw()
        {
            
        }

        public void Erase() {} // needs to be deleted once isprite is updated

        public Vector2 Center {

            get => new Vector2(offsetX, offsetY);

            set {
                offsetX = (int) value.X;
                offsetY = (int) value.Y;
            }
        }

        public Texture2D Texture {

            get => map;
        }

        public ref Rectangle Box {

            get => ref mapPos;
        }
    }
}
