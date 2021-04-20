using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Rooms;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class DoorSpriteFactory : ISpriteFactory
    {
        private Dictionary<string, Texture2D> doorTextures;
        private Dictionary<IDoor.DoorState, int> doorStateRectangleMap;
        private List<List<Rectangle>> doorRectangles;

        private static DoorSpriteFactory doorSpriteFactoryInstance = new DoorSpriteFactory();

        public static DoorSpriteFactory Instance
        {
            get => doorSpriteFactoryInstance;
        }

        private DoorSpriteFactory()
        {
            doorTextures = new Dictionary<string, Texture2D>();
            doorStateRectangleMap = new Dictionary<IDoor.DoorState, int>();
            doorRectangles = new List<List<Rectangle>>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            doorTextures.Add("doors", content.Load<Texture2D>("doors"));
            DungeonMask.Instance.addTexture(doorTextures["doors"]);
            doorTextures.Add("staircase", content.Load<Texture2D>("staircase"));
            DungeonMask.Instance.addTexture(doorTextures["staircase"]);
            doorTextures.Add("black", content.Load<Texture2D>("black"));

            doorStateRectangleMap.Add(IDoor.DoorState.Wall, 0);
            doorStateRectangleMap.Add(IDoor.DoorState.Open, 1);
            doorStateRectangleMap.Add(IDoor.DoorState.Locked, 2);
            doorStateRectangleMap.Add(IDoor.DoorState.Closed, 3);
            doorStateRectangleMap.Add(IDoor.DoorState.Bombed, 4);

            PopulateRectangleDoors();
        }

        private void PopulateRectangleDoors()
        {
            int rows = 4;
            int columns = doorStateRectangleMap.Count;

            int width = doorTextures["doors"].Width / columns;
            int height = doorTextures["doors"].Height / rows;

            for (int i=0; i<rows; i++)
            {
                doorRectangles.Add(new List<Rectangle>());
                for (int j=0; j<columns; j++)
                {
                    doorRectangles[i].Add(new Rectangle(width * j, height * i, width, height));
                }
            }
        }

        public IDoorSprite CreateUpDoorSprite(SpriteBatch spriteBatch, Vector2 startingPos, IDoor.DoorState doorState)
        {
            Rectangle doorRectangle = doorRectangles[0][doorStateRectangleMap[doorState]];
            if (doorState == IDoor.DoorState.Open || doorState == IDoor.DoorState.Bombed) return new NormalUpOpenDoorSprite(spriteBatch, doorTextures["black"], doorTextures["doors"], startingPos, doorRectangle, doorState == IDoor.DoorState.Bombed);
            return new NormalNonOpenDoorSprite(spriteBatch, doorTextures["doors"], startingPos, doorRectangle);
        }

        public IDoorSprite CreateLeftDoorSprite(SpriteBatch spriteBatch, Vector2 startingPos, IDoor.DoorState doorState)
        {
            Rectangle doorRectangle = doorRectangles[1][doorStateRectangleMap[doorState]];
            if (doorState == IDoor.DoorState.Open || doorState == IDoor.DoorState.Bombed) return new NormalLeftOpenDoorSprite(spriteBatch, doorTextures["black"], doorTextures["doors"], startingPos, doorRectangle, doorState == IDoor.DoorState.Bombed);
            return new NormalNonOpenDoorSprite(spriteBatch, doorTextures["doors"], startingPos, doorRectangle);
        }

        public IDoorSprite CreateRightDoorSprite(SpriteBatch spriteBatch, Vector2 startingPos, IDoor.DoorState doorState)
        {
            Rectangle doorRectangle = doorRectangles[2][doorStateRectangleMap[doorState]];
            if (doorState == IDoor.DoorState.Open || doorState == IDoor.DoorState.Bombed) return new NormalRightOpenDoorSprite(spriteBatch, doorTextures["black"], doorTextures["doors"], startingPos, doorRectangle, doorState == IDoor.DoorState.Bombed);
            return new NormalNonOpenDoorSprite(spriteBatch, doorTextures["doors"], startingPos, doorRectangle);
        }

        public IDoorSprite CreateDownDoorSprite(SpriteBatch spriteBatch, Vector2 startingPos, IDoor.DoorState doorState)
        {
            Rectangle doorRectangle = doorRectangles[3][doorStateRectangleMap[doorState]];
            if (doorState == IDoor.DoorState.Open || doorState == IDoor.DoorState.Bombed) return new NormalDownOpenDoorSprite(spriteBatch, doorTextures["black"], doorTextures["doors"], startingPos, doorRectangle, doorState == IDoor.DoorState.Bombed);
            return new NormalNonOpenDoorSprite(spriteBatch, doorTextures["doors"], startingPos, doorRectangle);
        }

        public IDoorSprite CreateStaircaseSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new StaircaseDoorSprite(spriteBatch, doorTextures["staircase"], startingPos);
        }

        public IDoorSprite CreatePortalSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new StaircaseDoorSprite(spriteBatch, doorTextures["staircase"], startingPos);
        }
    }
}
