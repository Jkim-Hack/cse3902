using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static cse3902.Sprites.LinkSprite;

namespace cse3902.Sprites
{
    public class LinkSpriteAnimationHandler
    {
        private Rectangle[] frames;
        private int totalFrames;
        private int frameWidth;
        private int frameHeight;

        private Vector2 frameSize;

        private bool isDamage;

        // AnimationState, (animation sequence, delay per frame)
        private Dictionary<AnimationState, (int[] frame, float[] delay)> frameSetIndicies;

        public LinkSpriteAnimationHandler(Texture2D texture, int rows, int columns)

        {
            totalFrames = rows * columns;
            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;

            frames = new Rectangle[totalFrames];
            frameSize = new Vector2(frameWidth, frameHeight);

            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);
            frameSetIndicies = LinkConstants.generateFrameSets();

            isDamage = false;
        }

        public (Rectangle, float)[] getFrameSet(AnimationState animationState)
        {
            List<(Rectangle, float)> frameSet = new List<(Rectangle, float)>();

            var indicies = frameSetIndicies[animationState];

            for (int i = 0; i < indicies.frame.Length; i++)
            {
                var frameTuple = (frames[indicies.frame[i]], frameSetIndicies[animationState].delay[i]);
                frameSet.Add(frameTuple);
            }
            return frameSet.ToArray();
        }

        public Vector2 FrameSize
        {
            get => frameSize;
        }

        public bool Damage
        {
            get => isDamage;
            set => isDamage = value;
        }
    }
}