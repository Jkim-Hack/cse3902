using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using cse3902.Collision;
using System.Collections;
using cse3902.Sprites;
using cse3902.Entities.DamageMasks;
using cse3902.Constants;

namespace cse3902.Rooms
{

    public class Background
    {
        private Texture2D interior;
        private Texture2D exterior;
        private Texture2D exterior2;
        private Texture2D itemRoom;
        private SpriteBatch batch;
        private List<ISprite> background;

        private IList walls;



        private static Background instance = new Background();

        public static Background Instance
        {
            get
            {
                return instance;
            }
        }

        private Background()
        {

        }

        public void LoadTextures(ContentManager content, SpriteBatch batch)
        {
            Texture2D mask = content.Load<Texture2D>("DungeonMask");
            DungeonMask.Instance.setMaskTexture(mask, SpriteConstants.DungeonMaskRows, SpriteConstants.DungeonMaskCols, SpriteConstants.DungeonMaskStartingIndex);
            interior = content.Load<Texture2D>("interior");
            DungeonMask.Instance.addTexture(interior);
            exterior = content.Load<Texture2D>("Exterior");
            DungeonMask.Instance.addTexture(exterior);
            exterior2 = content.Load<Texture2D>("Exterior2");
            DungeonMask.Instance.addTexture(exterior2);
            itemRoom = content.Load<Texture2D>("ItemRoom");
            DungeonMask.Instance.addTexture(itemRoom);

            this.batch = batch;
            background = new List<ISprite>();
            walls = new List<Wall>();
        }

        public void generateRoom(Vector3 loc, int roomNum)
        {
            Vector2 roomCenter = RoomUtilities.CalculateRoomCenter(loc);
            background.Add(new ExteriorSprite(batch, exterior, roomCenter, SpriteUtilities.BackgroundLayer));
            background.Add(new ExteriorSprite(batch, exterior2, roomCenter, SpriteUtilities.TopBackgroundLayer));
            background.Add(new InteriorSprite(batch, interior, RoomUtilities.INTERIOR_TEXTURE_ROWS, RoomUtilities.INTERIOR_TEXTURE_COLS, roomCenter, roomNum));
            foreach(Rectangle rec in RoomUtilities.GetWallRectangles(loc)){
                walls.Add(new Wall(rec));
            }
        }

        public void generateItemRoom(Vector3 loc)
        {
            Vector2 roomCenter = RoomUtilities.CalculateRoomCenter(loc);
            background.Add(new ExteriorSprite(batch, itemRoom, roomCenter, SpriteUtilities.BackgroundLayer));
        }

        public void generateRoomWall(Vector2 blockLoc)
        {
            walls.Add(new Wall(RoomUtilities.calculateRoomBlockWall(blockLoc)));
        }


        public void Update(GameTime gameTime)
        {
            foreach (ISprite item in background)
            {
                item.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (ISprite item in background)
            {
                item.Draw();
            }
        }



        public ref IList WallsListRef
        {
            get => ref walls;
        }

    }
}
