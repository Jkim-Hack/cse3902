using System;
using System.Collections.Generic;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.DamageMasks
{
    public class SingleMaskHandler
    {
        private Texture2D spriteTexture;
        private Texture2D maskTexture;

        private Color[] originalData;

        private Rectangle maskFrame;
        private Rectangle originalFrame;

        private int totalColors;

        private List<Color[]> textureColorSequence;
	    private List<Color[]> allMaskColors;

        public SingleMaskHandler(Texture2D spriteTexture, Texture2D maskTexture)
        {
            this.spriteTexture = spriteTexture;
            this.maskTexture = maskTexture;

            int frameWidth = maskTexture.Width;
            int frameHeight = maskTexture.Height;
            maskFrame = SpriteUtilities.distributeFrames(1, 1, frameWidth, frameHeight)[0];

            // FOR LINK SPECIFICALLY TODO: MAKE THIS FOR ANY SPRITE
            frameWidth = spriteTexture.Width / 6;
            frameHeight = spriteTexture.Height / 4;
            originalFrame = SpriteUtilities.distributeFrames(6, 4, frameWidth, frameHeight)[6];

            originalData = new Color[spriteTexture.Width * spriteTexture.Height];
            spriteTexture.GetData(originalData);

            allMaskColors = new List<Color[]>();
            textureColorSequence = new List<Color[]>();
            textureColorSequence.Add(originalData);
            AddAllColorMasks();

            GenerateTextureMasks();
            Reset();
        }

        private void AddColorMask(Color[] maskColors)
        {
            allMaskColors.Add(maskColors);
            textureColorSequence.Add(new Color[originalData.Length]);
        }

        private void AddOriginalColorMask()
        {
            int frameSize = (spriteTexture.Width / 6) * (spriteTexture.Height / 4);
            Color[] frameColors = new Color[frameSize];

            spriteTexture.GetData(0, originalFrame, frameColors, 0, frameSize);

            List<Color> colors = new List<Color>();
            foreach (var color in frameColors)
            {
                if (!colors.Contains(color) && !color.Equals(Color.Transparent))
                {
                    colors.Add(color);
                }
            }
            totalColors = colors.Count;

            AddColorMask(colors.ToArray());
        }

        // Textures go from top down
        private void AddAllColorMasks()
        {
            AddOriginalColorMask();
            int frameSize = maskFrame.Width * maskFrame.Height;
            Color[] frameColors = new Color[frameSize];

            maskTexture.GetData(0, maskFrame, frameColors, 0, frameSize);

            List<Color> colors = new List<Color>();
            foreach (var color in frameColors)
            {
                if (!colors.Contains(color) && !color.Equals(Color.Transparent))
                {
                    colors.Add(color);
                }
            }
            totalColors = colors.Count;

            AddColorMask(colors.ToArray());
        }

        private void GenerateTextureMasks()
        {
            textureColorSequence[0] = originalData;
            for (int j = 0; j < originalData.Length; j++)
            {
                for (int k = 0; k < totalColors; k++)
                {
                    if (originalData[j] == allMaskColors[0][k])
                    {
                        Color maskColor = allMaskColors[1][k];
                        textureColorSequence[1][j] = maskColor;
                    }
                }
            }
        }

        public void LoadMask()
        {
            Console.WriteLine(textureColorSequence[1]);
            if (textureColorSequence.Count > 1)
                spriteTexture.SetData(textureColorSequence[1]);
        }

        public void Reset()
        {
            spriteTexture.SetData(textureColorSequence[0]);
        }
    }
}
