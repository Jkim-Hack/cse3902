using System;
using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cse3902
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch { get; set; }

        private ISprite textSprite;
        public List<ISprite> spriteList { get; set; }
        List<IController> controllerList;

        public int currentSpriteIndex { get; set; }

        IEntity player;

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

            // Initialize sprite list
            spriteList = new List<ISprite>();
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

            // Set up sprites
	        Texture2D spriteTexture = this.Content.Load<Texture2D>("mario-yoshi");    
	        spriteList.Add(new StaticStillSprite(spriteBatch, spriteTexture, 1, 2));
	        spriteList.Add(new AnimatedStillSprite(spriteBatch, spriteTexture, 1, 2));
	        spriteList.Add(new StaticMovableSprite(spriteBatch, spriteTexture, 1, 2));
	        spriteList.Add(new AnimatedMovableSprite(spriteBatch, spriteTexture, 1, 2));

            SpriteFont textFont = this.Content.Load<SpriteFont>("Credits");
            textSprite = new TextSprite(spriteBatch, textFont, "Credits\nProgram Made By: John Kim\nSprites from: http://www.mariouniverse.com/sprites-nes-yoshi/");
	        // The sprite index should initially be out of bounds so that there are no premature calls
            currentSpriteIndex = spriteList.Count;
            player = new Link(this);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
            if (currentSpriteIndex < spriteList.Count)
            {
                spriteList[currentSpriteIndex].Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw sitself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentSpriteIndex < spriteList.Count)
            {
                spriteList[currentSpriteIndex].Draw();
            }
            textSprite.Draw();

            base.Draw(gameTime);
        }
    }
}
