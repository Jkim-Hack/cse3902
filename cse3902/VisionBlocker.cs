using Microsoft.Xna.Framework;
using cse3902.Constants;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;

namespace cse3902
{
    public class VisionBlocker
    {
        private bool visionBlocked;
        private bool triforce;

        /*
         * sideOffset[0] = bottom
         * sideOffset[1] = left
         * sideOffset[2] = top
         * sideOffset[3] = right
         */
        private List<Vector2> sideOffset;
        private List<Vector2> visionRange;

        private Vector2 linkPos;
        private Vector2 windowDimensions;
        private Texture2D rectangleTexture;

        private Game1 game;

        private static VisionBlocker visionBlockerInstance = new VisionBlocker();
        public static VisionBlocker Instance
        {
            get => visionBlockerInstance;
        }
        private VisionBlocker()
        {
            visionBlocked = false;
            linkPos = new Vector2();
            windowDimensions = new Vector2(DimensionConstants.OriginalWindowWidth, DimensionConstants.OriginalGameplayHeight);
            visionRange = new List<Vector2>()
            {
                new Vector2(0, 1),
                new Vector2(-1, 0),
                new Vector2(0, -1),
                new Vector2(1, 0)
            };
        }

        public void Update()
        {
            linkPos = game.Player.Center;
        }
        public void Draw()
        {
            if (!triforce)
            {
                int visionRadius = SettingsValues.Instance.GetValue(SettingsValues.Variable.VisionRange);
                if (visionBlocked) visionRadius = SettingsValues.Instance.GetValue(SettingsValues.Variable.VisionRangeNoCandle);
                for (int i=0; i<sideOffset.Count; i++)
                {
                    Rectangle r = new Rectangle((linkPos + sideOffset[i] + visionRadius * visionRange[i]).ToPoint(), (2*windowDimensions).ToPoint());
                    game.SpriteBatch.Draw(rectangleTexture, r, null, Color.Black, 0, new Vector2(), SpriteEffects.None, SpriteUtilities.VisionBlockLayer);
                }            
            }
        }

        public bool VisionIsBlocked
        {
            set => visionBlocked = value;
        }
        public bool Triforce
        {
            set => triforce = value;
        }
        public Game1 Game
        {
            set
            {
                game = value;
                Vector2 linkSize = game.Player.Size / 2;
                rectangleTexture = new Texture2D(game.GraphicsDevice, 1, 1);
                rectangleTexture.SetData(new Color[] { Color.White });

                sideOffset = new List<Vector2>();
                sideOffset.Add(new Vector2(-windowDimensions.X, linkSize.Y));
                sideOffset.Add(new Vector2(- (2 * windowDimensions.X + linkSize.X), - (linkSize.Y + windowDimensions.Y)));
                sideOffset.Add(new Vector2(- (linkSize.X + windowDimensions.X), - (2 * windowDimensions.Y + linkSize.Y)));
                sideOffset.Add(new Vector2(linkSize.X, -windowDimensions.Y));
            }
        }
    }
}