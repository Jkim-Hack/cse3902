using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Interfaces;
using cse3902.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public ItemHandler itemHandler { get; set; }
        public EnemyNPCHandler enemyNPCHandler { get; set; }
        public BlockHandler blockHandler { get; set; }

        public IPlayer player { get; set; }

        public List<IProjectile> linkProjectiles { get; set; }

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

            itemHandler = new ItemHandler();
            enemyNPCHandler = new EnemyNPCHandler(this);
            blockHandler = new BlockHandler(this);

            linkProjectiles = new List<IProjectile>();

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

            itemHandler.LoadContent(spriteBatch, Content);
            enemyNPCHandler.LoadContent();
            blockHandler.LoadContent();
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

            for (int i = 0; i < linkProjectiles.Count; i++) 
            {
                IProjectile projectile = linkProjectiles[i];
                projectile.Update(gameTime);
                if (projectile.AnimationComplete)
                {
                    linkProjectiles.Remove(projectile);
                    i--;
                }
            }

            player.Update(gameTime);

            itemHandler.Update(gameTime);
            enemyNPCHandler.Update(gameTime);
            blockHandler.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw sitself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (IProjectile projectile in linkProjectiles)
            {
                projectile.Draw();
            }
            itemHandler.Draw();
            player.Draw();
            spriteBatch.End();
            enemyNPCHandler.Draw();
            blockHandler.Draw();
            base.Draw(gameTime);
        }
    }
}
