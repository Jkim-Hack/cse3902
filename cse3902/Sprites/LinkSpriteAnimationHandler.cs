using System;
using cse3902.Interfaces;
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

        private const int damageMaskCount = 3;
        private const int spriteSetCount = 24;
       
        // AnimationState, animation sequence
	    private Dictionary<AnimationState, int[]> frameSetIndicies;
        
	    public LinkSpriteAnimationHandler(Texture2D texture, int rows, int columns, AnimationState startingAnimState)
        {
            totalFrames = rows * columns;
            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;

            frames = new Rectangle[totalFrames];
            frameSize = new Vector2(frameWidth, frameHeight);

	        distributeFrames(columns);
	        generateFrameSets();

            isDamage = false;

        }

        private void distributeFrames(int columns)
        {
            for (int i = 0; i < totalFrames; i++)
            {
                int row = (int)((float)i / (float)columns);
                int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }
        }
        
        private void generateFrameSets()
        {
            frameSetIndicies = new Dictionary<AnimationState, int[]>()
            {
                { AnimationState.LeftFacing, new int[]    { 2 } },
                { AnimationState.LeftRunning, new int[]   { 2, 3} },
                { AnimationState.RightFacing, new int[]   { 0 } },
                { AnimationState.RightRunning, new int[]  { 0, 1 } },
                { AnimationState.UpFacing, new int[]      { 4 } },
                { AnimationState.UpRunning, new int[]     { 4, 5} },
                { AnimationState.DownFacing, new int[]    { 6 } },
                { AnimationState.DownRunning, new int[]   { 6, 7 } },
                { AnimationState.LeftAttack, new int[]    { 9, 3, 2 } },
                { AnimationState.RightAttack, new int[]   { 8, 1, 0 } },
                { AnimationState.UpAttack, new int[]      { 10, 4, 5 } },
                { AnimationState.DownAttack, new int[]    { 11, 6, 7 } },
            }; 
	    }

        public Rectangle[] getFrameSet(AnimationState animationState)
        {
            List<Rectangle> frameSet = new List<Rectangle>();
	        int[] indicies = frameSetIndicies[animationState];
            
	        if (isDamage)
            {
                for (int i = 0; i < indicies.Length; i++)
                {
                    for (int j = 1; j <= damageMaskCount; j++)
                    {
                        int frameIndex = indicies[i] + spriteSetCount * j;
                        frameSet.Add(frames[frameIndex]);
                    }
                }
            }
            else
            { 
                for (int i = 0; i < indicies.Length; i++)
                {
                   frameSet.Add(frames[indicies[i]]);
                }
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
