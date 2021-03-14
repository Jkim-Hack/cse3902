using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Interfaces;
using cse3902.Items;
using cse3902.Projectiles;
using cse3902.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.SpriteFactory;
using cse3902.Collision;
using System.Linq;
using System;

namespace cse3902
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
     GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch { get; set; }

        List<IController> controllerList;

        private AllCollidablesList allCollidablesList;

        public ItemHandler itemHandler { get; set; }
        public EnemyNPCHandler enemyNPCHandler { get; set; }
        public BlockHandler blockHandler { get; set; }

        public RoomHandler roomHandler;

        public IPlayer player { get; set; }

        public ProjectileHandler projectileHandler { get; set; }

        public AllCollidablesList AllCollidablesList { get => this.allCollidablesList; }

        public CollisionManager collisionManager { get; set; }

        private Texture2D lineTexture;

        public Camera camera { get; set;  }
        
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

            itemHandler = new ItemHandler();
            projectileHandler = ProjectileHandler.Instance;
            enemyNPCHandler = new EnemyNPCHandler(this);
            blockHandler = new BlockHandler(this);
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

            //camera.MoveCamera(new Vector2(256, 0), new Vector2(256, 176));

            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            DoorSpriteFactory.Instance.LoadAllTextures(Content);
            RoomBackground.Instance.LoadTextures(Content, spriteBatch);

            //RoomBackground.Instance.generateRoom(new Vector3(1, 0, 0), 1);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            projectileHandler.LoadAllTextures(Content);

            itemHandler.LoadContent(spriteBatch, Content);
            enemyNPCHandler.LoadContent();
            blockHandler.LoadContent();

            // For hitbox drawing
	          lineTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
	          lineTexture.SetData<Color>(new Color[] { Color.White });

            // Testing purposes
            RoomBackground.Instance.generateRoom(new Vector3(0,0,0), 1);

            collisionManager = new CollisionManager(this);

            allCollidablesList.Insert((int)CollisionManager.CollisionPriority.PLAYER, player);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.ENEMY_NPC, ref RoomEnemyNPCs.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.ITEMS, ref RoomItems.Instance.ListRef);
            allCollidablesList.InsertNewList((int)CollisionManager.CollisionPriority.BLOCKS, ref RoomBlocks.Instance.ListRef);

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

            projectileHandler.Update(gameTime);
            
	          player.Update(gameTime);

            RoomBackground.Instance.Update(gameTime);
            collisionManager.Update();
            RoomItems.Instance.Update(gameTime);
            RoomEnemyNPCs.Instance.Update(gameTime);
            RoomBlocks.Instance.Update(gameTime);

            roomHandler.Update();

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

            projectileHandler.Draw();
            player.Draw();
            RoomBackground.Instance.Draw();
            RoomItems.Instance.Draw();
            RoomEnemyNPCs.Instance.Draw();
            RoomBlocks.Instance.Draw();

            roomHandler.Draw();

            collisionManager.DrawAllRectangles(lineTexture, Color.Red, 1);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}