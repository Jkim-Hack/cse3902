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

        private Rectangle frame;

        private int totalColors;
        private int totalMasks;

        private List<Color[]> textureColorSequence;
	    private List<Color[]> allMaskColors;

        public SingleMaskHandler(Texture2D spriteTexture, Texture2D maskTexture)
        {
            this.spriteTexture = spriteTexture;
            this.maskTexture = maskTexture;

            int frameWidth = maskTexture.Width;
            int frameHeight = maskTexture.Height;
            frame = SpriteUtilities.distributeFrames(1, 1, frameWidth, frameHeight)[0];

            originalData = new Color[spriteTexture.Width * spriteTexture.Height];
            spriteTexture.GetData(originalData);

            allMaskColors = new List<Color[]>();
            textureColorSequence = new List<Color[]>();
            AddAllColorMasks();

            GenerateTextureMasks();
            Reset();
        }

        private void AddColorMask(Color[] maskColors)
        {
            allMaskColors.Add(maskColors);
            textureColorSequence.Add(new Color[originalData.Length]);
        }

        // Textures go from top down
        private void AddAllColorMasks()
        {
            int frameSize = frame.Width * frame.Height;
            Color[] frameColors = new Color[frameSize];

            maskTexture.GetData(0, frame, frameColors, 0, frameSize);

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
                    for (int i = 1; i < totalMasks; i++)
                    {
                        if (originalData[j] == allMaskColors[0][k])
                        {
                            Color maskColor = allMaskColors[i][k];
                            textureColorSequence[i][j] = maskColor;
                        }
                    }
                }
            }
        }

        public void LoadMask()
        {
            if (textureColorSequence.Count > 1)
                spriteTexture.SetData(textureColorSequence[1]);
        }

        public void Reset()
        {
            spriteTexture.SetData(textureColorSequence[0]);
        }
    }
}
