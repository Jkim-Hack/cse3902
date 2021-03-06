﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static cse3902.Sprites.LinkSprite;

namespace cse3902.Constants
{
    public class LinkConstants
    {
	    public const int defaultXDirection = 1;
        public const int defaultYDirection = 1;
        public const float defaultSpeed = 50f;
        public const int defaultShoveDistance = -10;
        public const float hitboxSizeModifier = 0.75f;
        public const int defaultSoundDelay = 20;

        private const float defaultDelay = 0.2f;


        public const int LinkTextureRows = 6;
        public const int LinkTextureCols = 4;
        public const int DamageMaskRows = 1;
        public const int DamageMaskCols = 4;

        public const int LinkShoveDistance = 20;
        public const int LinkShoveDistanceGrabbed = 100;

        public const float ItemDistance = 1.5f;


        public static Dictionary<AnimationState, (int[] frame, float[] delay)> generateFrameSets()
        {
            var frameSetIndicies = new Dictionary<AnimationState, (int[] frame, float[] delay)>()
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
                { AnimationState.Item,        ( new int[]  { 21, 20, 21 },     new float[] { 0.1f, 1.4f, 0.1f }) },
                { AnimationState.GameWon,     ( new int[]  { 21 },             new float[] { 10.0f }) },
                { AnimationState.Death,       ( new int[]  { 6, 0, 4, 2, 6, 0, 4, 2, 6 },new float[] { defaultDelay, defaultDelay, defaultDelay, defaultDelay, defaultDelay, defaultDelay, defaultDelay, defaultDelay, defaultDelay }) }
            };
            return frameSetIndicies;
        }
    }
}
