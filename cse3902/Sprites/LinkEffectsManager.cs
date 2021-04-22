using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using cse3902.Entities.DamageMasks;
using cse3902.Constants;

namespace cse3902.Sprites
{
    public class LinkEffectsManager
    {
        private SpriteBatch spriteBatch;
        private Texture2D rectangleTexture;
      
	    private bool gameWon;
        private int gameWinFlashDelay;
        private int gameWinRectangleWidth;

        private bool death;
        private int deathColorChangeDelay;
        private Color deathColor;
        private float deathOpacity;
        
	    private bool isDamaged;
        private float remainingDamageDelay;

        private (GenericTextureMask genericMaskHandler, SingleMaskHandler deathMaskHandler) maskHandlers;

        public LinkEffectsManager(SpriteBatch spriteBatch, Texture2D rectangle, GenericTextureMask maskHandler, SingleMaskHandler singleMaskHandler)
        {
            this.spriteBatch = spriteBatch;
            rectangleTexture = rectangle;
            rectangleTexture.SetData(new Color[] { Color.White });
            
	        maskHandlers.genericMaskHandler = maskHandler;
            maskHandlers.deathMaskHandler = singleMaskHandler;

            gameWon = false;
            gameWinFlashDelay = -50;
            gameWinRectangleWidth = 0;

            deathColorChangeDelay = 0;
            deathColor = Color.Black;
            deathOpacity = 0;

            remainingDamageDelay = -1;
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
	        if (isDamaged && !maskHandlers.genericMaskHandler.Disabled)
            {
                remainingDamageDelay -= timer;
                if (remainingDamageDelay < 0)
                {
                    remainingDamageDelay = DamageConstants.DamageMaskDelay;
                    maskHandlers.genericMaskHandler.LoadNextMask();
                }
            }
            
	        if (gameWinFlashDelay > -50) gameWinFlashDelay--;
            if (gameWon && gameWinFlashDelay == -50 && gameWinRectangleWidth < DimensionConstants.OriginalWindowWidth) gameWinRectangleWidth++;

            if (deathColorChangeDelay > 0) deathColorChangeDelay--;
            else death = false;
        }

        public void Draw()
        {
            int roomWidth = DimensionConstants.OriginalWindowWidth;
            int roomHeight = DimensionConstants.OriginalGameplayHeight;
            DrawGameWinAnim(roomWidth, roomHeight);
            DrawDeathAnim(roomWidth, roomHeight);
        }

        private void DrawGameWinAnim(int width, int height) /* position will need to be changed when triforce is moved */
        {
            if (gameWinFlashDelay > 0 && gameWinFlashDelay <= 60 && ((gameWinFlashDelay / 5) % 2 == 0))
            {
                DrawRectangle(Color.White * 0.75f, new Rectangle(RoomUtilities.TriforceRoomPoint, new Point(width, height)), SpriteUtilities.GameWonLayer);
            }

            if (gameWon)
            {
                DrawRectangle(Color.Black, new Rectangle(RoomUtilities.TriforceRoomPoint, new Point(gameWinRectangleWidth, height)), SpriteUtilities.GameWonLayer);
                DrawRectangle(Color.Black, new Rectangle(RoomUtilities.TriforceRoomPoint.X + width - gameWinRectangleWidth, RoomUtilities.TriforceRoomPoint.Y, gameWinRectangleWidth, height), SpriteUtilities.GameWonLayer);
            }
        }

        private void DrawDeathAnim(int width, int height)
        {
            if (deathColorChangeDelay > 0)
            {
                if (deathColorChangeDelay == 30) deathOpacity = 1.0f;
                if ((deathColorChangeDelay - 1) % 27 == 0) deathColor.R -= 255 / 4;

                DrawRectangle(deathColor * deathOpacity, new Rectangle(-1500, 0, width * 60, height * 6), SpriteUtilities.DeathEffectLayer);
            }
        }

        private void DrawRectangle(Color color, Rectangle destination, float layer)
        {
            spriteBatch.Draw(rectangleTexture, destination, null, color, 0, new Vector2(), SpriteEffects.None, layer);
        }

        public void SetGameWon(bool state)
        {
            gameWon = state;
            if (state)
            {
                gameWinFlashDelay = 100;
                gameWinRectangleWidth = 0;
            }
        }

        public void SetDeath()
        {
            death = true;
            deathColorChangeDelay = 135;
            deathColor = Color.Red;
            deathOpacity = 0.75f;
        }
        
	    public bool Damaged
        {
            get => isDamaged;
            set 
	        {
                isDamaged = value;
                remainingDamageDelay = DamageConstants.DamageMaskDelay;
                maskHandlers.genericMaskHandler.Reset();
            }
        }
        
        public bool IsTopLayer
        {
            get => gameWon || death;
        }

	    public GenericTextureMask DamageMaskHandler
        {
            get => maskHandlers.genericMaskHandler;
        }

        public SingleMaskHandler DeathMaskHandler
        {
            get => maskHandlers.deathMaskHandler;
        }
    }
}
