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

        private const float defaultDelay = 0.2f;

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
            generateFrameSets();

            isDamage = false;

        }

        private void generateFrameSets()
        {
            frameSetIndicies = new Dictionary<AnimationState, (int[] frame, float[] delay)>()
            {
                { AnimationState.LeftFacing,  ( new int[]  { 2 },              new float[] { defaultDelay }) },
                { AnimationState.LeftRunning, ( new int[]  { 2, 3 },           new float[] { defaultDelay, defaultDelay }) },
                { AnimationState.RightFacing, ( new int[]  { 0 },              new float[] { defaultDelay }) },
                { AnimationState.RightRunning,( new int[]  { 0, 1 },           new float[] { defaultDelay, defaultDelay }) },
                { AnimationState.UpFacing,    ( new int[]  { 4 },              new float[] { defaultDelay }) },
                { AnimationState.UpRunning,   ( new int[]  { 4, 5 },           new float[] { defaultDelay, defaultDelay }) },
                { AnimationState.DownFacing,  ( new int[]  { 6 },              new float[] { defaultDelay }) },
                { AnimationState.DownRunning, ( new int[]  { 6, 7 },           new float[] { defaultDelay, defaultDelay }) },
                { AnimationState.LeftAttack,  ( new int[]  { 9, 9, 3, 2 },     new float[] { 0.1f, 0.15f, 0.05f, 0.05f }) },
                { AnimationState.RightAttack, ( new int[]  { 8, 8, 1, 0 },     new float[] { 0.1f, 0.15f, 0.05f, 0.05f }) },
                { AnimationState.UpAttack,    ( new int[]  { 10, 10, 4, 5 },   new float[] { 0.1f, 0.15f, 0.05f, 0.05f }) },
                { AnimationState.DownAttack,  ( new int[]  { 11, 11, 6, 7 },   new float[] { 0.1f, 0.15f, 0.05f, 0.05f }) },
            }; 
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