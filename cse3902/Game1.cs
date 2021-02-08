﻿using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.Items;
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
        SpriteBatch spriteBatch;

        private ISprite textSprite;
        public List<ISprite> spriteList { get; set; }
        List<IController> controllerList;

        public List<ISprite> items;
        private int itemIndex = 0;
        private int itemDuration = 0;

        private ISprite arrow;
        private ISprite bomb;

        public int currentSpriteIndex { get; set; }

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

            items = new List<ISprite>();

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


            //Texture2D arrowSprite = this.Content.Load<Texture2D>("arrow");
            //Texture2D bombExplode = this.Content.Load<Texture2D>("bombexplode");

            items.Add(new ArrowItem(spriteBatch, this.Content.Load<Texture2D>("arrow"), new Vector2(100, 100)));
            items.Add(new BombItem(spriteBatch, this.Content.Load<Texture2D>("bombnew"), new Vector2(100, 100)));
            items.Add(new BoomerangItem(spriteBatch, this.Content.Load<Texture2D>("boomerang"), new Vector2(100, 100)));
            items.Add(new BowItem(spriteBatch, this.Content.Load<Texture2D>("bow"), new Vector2(100, 100)));
            items.Add(new ClockItem(spriteBatch, this.Content.Load<Texture2D>("clock"), new Vector2(100, 100)));
            items.Add(new FairyItem(spriteBatch, this.Content.Load<Texture2D>("fairy"), new Vector2(100, 100)));
            items.Add(new CompassItem(spriteBatch, this.Content.Load<Texture2D>("compass"), new Vector2(100, 100)));
            items.Add(new HeartItem(spriteBatch, this.Content.Load<Texture2D>("heart"), new Vector2(100, 100)));
            items.Add(new HeartContainerItem(spriteBatch, this.Content.Load<Texture2D>("heartcont"), new Vector2(100, 100)));
            items.Add(new TriforceItem(spriteBatch, this.Content.Load<Texture2D>("triforce"), new Vector2(100, 100)));
            items.Add(new KeyItem(spriteBatch, this.Content.Load<Texture2D>("key"), new Vector2(100, 100)));
            items.Add(new MapItem(spriteBatch, this.Content.Load<Texture2D>("map"), new Vector2(100, 100)));


            foreach (ISprite item in items)
            {
                item.StartingPosition = new Vector2(100, 100);
            }

            
            //arrow = new ArrowItem(spriteBatch, arrowSprite, new Vector2(100, 100));
            //arrow.StartingPosition = new Vector2(100, 100);

            //Texture2D bombExplode = this.Content.Load<Texture2D>("bombexplode");
            //bomb = new BombItem(spriteBatch, bombExplode, new Vector2(100, 100));
            //bomb.StartingPosition = new Vector2(200, 100);

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

            if (itemDuration < 100)
            {
                items[itemIndex].Update(gameTime);
                itemDuration++;
            }
            else
            {
                itemDuration = 0;
                itemIndex++;
                if (itemIndex >= items.Count)
                {
                    itemIndex = 0;
                }
            }

            //arrow.Update(gameTime);
            //bomb.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw sitself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            if (currentSpriteIndex < spriteList.Count)
            {
                spriteList[currentSpriteIndex].Draw();
            }
            textSprite.Draw();

            items[itemIndex].Draw();

            //arrow.Draw();
            //bomb.Draw();

            base.Draw(gameTime);
        }
    }
}
