﻿using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using cse3902.Collision;
using System.Collections;
using cse3902.Sprites;

namespace cse3902.Rooms
{
    //Rename this and 
    public class Background
    {
        private Texture2D interior;
        private Texture2D exterior;
        private Texture2D exterior2;
        private Texture2D itemRoom;
        private SpriteBatch batch;
        private List<ISprite> background;

        private IList walls;

        //private List<Rectangle> test { get; set; }
        //Texture2D test1;

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
            interior = content.Load<Texture2D>("interior");
            exterior = content.Load<Texture2D>("Exterior");
            exterior2 = content.Load<Texture2D>("Exterior2");
            itemRoom = content.Load<Texture2D>("ItemRoom");
            //test1 = content.Load<Texture2D>("block3");
            this.batch = batch;
            background = new List<ISprite>();
            walls = new List<Wall>();
            //test = new List<Rectangle>();
        }

        public void generateRoom(Vector3 loc, int roomNum)
        {
            Vector2 roomCenter = RoomUtilities.CalculateRoomCenter(loc);
            background.Add(new ExteriorSprite(batch, exterior, roomCenter, SpriteUtilities.BackgroundLayer));
            background.Add(new ExteriorSprite(batch, exterior2, roomCenter, SpriteUtilities.TopBackgroundLayer));
            background.Add(new InteriorSprite(batch, interior, RoomUtilities.INTERIOR_TEXTURE_ROWS, RoomUtilities.INTERIOR_TEXTURE_COLS, roomCenter, roomNum));
            foreach(Rectangle rec in RoomUtilities.GetWallRectangles(loc)){
                walls.Add(new Wall(rec));
                //test.Add(rec);
            }
        }

        public void generateItemRoom(Vector3 loc)
        {
            Vector2 roomCenter = RoomUtilities.CalculateRoomCenter(loc);
            background.Add(new ExteriorSprite(batch, itemRoom, roomCenter, SpriteUtilities.BackgroundLayer));
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

            /*foreach (Rectangle rec in test)
            {
                batch.Draw(test1, rec, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, .05f);
            }*/
        }

        /*public void LoadNewRoom(ref List<ISprite> oldSpriteList, List<ISprite> newSpriteList, ref List<ICollidable> oldWallList, List<ICollidable> newWallList)
        {
            oldSpriteList = new List<ISprite>();

            for (int i = 0; i < oldSpriteList.Count; i++)
            {
                oldSpriteList[i] = oldSpriteList[i];
            }

            background.Clear();

            for (int i = 0; i < newSpriteList.Count; i++)
            {
                background[i] = newSpriteList[i];
            }

            oldWallList = new List<ICollidable>();

            for (int i = 0; i < oldWallList.Count; i++)
            {
                oldWallList[i] = Walls[i];
            }

            Walls.Clear();

            for (int i = 0; i < newWallList.Count; i++)
            {
                Walls[i] = newWallList[i];
            }

        }*/

        public ref IList WallsListRef
        {
            get => ref walls;
        }

    }
}