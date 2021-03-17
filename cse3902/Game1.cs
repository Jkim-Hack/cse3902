using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Interfaces;
using cse3902.Projectiles;
using cse3902.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.SpriteFactory;
using cse3902.Collision;

namespace cse3902
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get => spriteBatch; }

        private List<IController> controllerList;

        private AllCollidablesList allCollidablesList;
        public AllCollidablesList AllCollidablesList { get => this.allCollidablesList; }

        private RoomHandler roomHandler;
        public RoomHandler RoomHandler { get => roomHandler; }

        private IPlayer player;
        public IPlayer Player { get => player; }

        private CollisionManager collisionManager;
        public CollisionManager CollisionManager { get => collisionManager; }

        private Camera camera;
        public Camera Camera { get => camera; }


        private Texture2D lineTexture;
        
	    public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Setup input controllers    
	        controllerList = new List<IController>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));

            allCollidablesList = new AllCollidablesList();

            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Link(this);
            camera = new Camera(this);
            roomHandler = new RoomHandler(this);
            collisionManager = new CollisionManager(this);

            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            DoorSpriteFactory.Instance.LoadAllTextures(Content);
            RoomBackground.Instance.LoadTextures(Content, spriteBatch);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            NPCSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileHandler.Instance.LoadAllTextures(Content);

            // For hitbox drawing
	        lineTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
	        lineTexture.SetData<Color>(new Color[] { Color.White });

            roomHandler.Initialize();

            collisionManager = new CollisionManager(this);

            allCollidablesList.Insert((int)CollisionManager.CollisionPriority.PLAYER, player);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.ENEMIES, ref RoomEnemies.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.NPCS, ref RoomNPCs.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.ITEMS, ref RoomItems.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.PROJECTILES, ref RoomProjectiles.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.BLOCKS, ref RoomBlocks.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.DOORS, ref RoomDoors.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.BACKGROUND, ref RoomBackground.Instance.WallsListRef);

            roomHandler.LoadNewRoom(roomHandler.startingRoomTranslation,0);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
    
	        player.Update(gameTime);
            roomHandler.Update(gameTime);
            collisionManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw sitself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.GetTransformationMatrix());

            player.Draw();
            roomHandler.Draw();
            collisionManager.DrawAllRectangles(lineTexture, Color.Red, 1);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}