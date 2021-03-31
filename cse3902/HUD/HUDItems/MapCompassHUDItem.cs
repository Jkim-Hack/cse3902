using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class MapCompassHUDItem : IHUDItem
    {
        private Game1 game;
        private Vector3 currentRoom;

        private Texture2D label;
        private Texture2D compass;

        private Rectangle labelPos;
        private Rectangle compassPos;
        
        private int offsetX;
        private int offsetY;

        private bool alreadyChanged;

        private Rectangle box;

        public CompassHUDItem(Game1 game, Texture2D label, Texture2D compass)
        {
            this.game = game;
            this.currentRoom = game.RoomHandler.currentRoom;
            this.currentRoom.Y++;

            this.label = label;
            this.compass = compass;

            this.offsetX = 0;
            this.offsetY = 0;

            this.labelPos = new Rectangle(offsetX, offsetY, label.Bounds.Width, label.Bounds.Height);
            this.compassPos = new Rectangle(offsetX + (label.Bounds.Width / 2) - (compass.Bounds.Width / 2), offsetY + label.Bounds.Height, compass.Bounds.Width, compass.Bounds.Height);

            this.alreadyChanged = false;

            box = new Rectangle();
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

            get => label;
        }

        public ref Rectangle Box {

            get => ref box;
        }
    }
}
