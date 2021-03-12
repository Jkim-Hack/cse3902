using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.DamageMasks
{
    public class DamageMaskHandler
    {
        private Texture2D spriteTexture;

        private Texture2D damageSequenceTexture;
        private List<Color[]> textureColorSequence;

        private Rectangle[] damageFrames;

        private Color[] originalData;

	    private List<Color[]> allMaskColors;
        private int totalColors;
        private int totalMasks;
        private int startingColorIndex;

        private int currentMaskIndex;

        public DamageMaskHandler(Texture2D spriteTexture, Texture2D damageSequenceTexture, int sequenceRows, int sequenceColumns, int startingColorIndex)
        {
            this.spriteTexture = spriteTexture;
	        this.damageSequenceTexture = damageSequenceTexture;

            int totalFrames = sequenceRows * sequenceColumns; 
            int frameWidth = damageSequenceTexture.Width / sequenceColumns;
            int frameHeight = damageSequenceTexture.Height / sequenceRows;
	        damageFrames = new Rectangle[totalFrames];
            distributeFrames(sequenceColumns, frameWidth, frameHeight, totalFrames);

            originalData = new Color[spriteTexture.Width * spriteTexture.Height];
            spriteTexture.GetData(originalData);

            this.startingColorIndex = startingColorIndex;

            allMaskColors = new List<Color[]>();
            textureColorSequence = new List<Color[]>();
            AddAllColorMasks();

            GenerateTextureMasks();
            Reset();
        }

        private void distributeFrames(int columns, int frameWidth, int frameHeight, int totalFrames)
        {
            for (int i = 0; i < totalFrames; i++)
            {
                int row = (int)((float)i / (float)columns);
                int column = i % columns;
                damageFrames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }
        }

        private void AddColorMask(Color[] maskColors)
        {
            allMaskColors.Add(maskColors);
            textureColorSequence.Add(new Color[originalData.Length]);
            totalMasks++;
        } 
       
	     // Textures go from top down
        private void AddAllColorMasks()
        {
            foreach (var frame in damageFrames)
            {
                int frameSize = frame.Width * frame.Height;
                Color[] frameColors = new Color[frameSize];

                damageSequenceTexture.GetData(0, frame, frameColors, 0, frameSize);

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
                        if (originalData[j] == allMaskColors[startingColorIndex][k])
                        {
                            Color maskColor = allMaskColors[i][k];
                            textureColorSequence[i][j] = maskColor;
                        }
                    }
                }
            }
        }

        public void LoadNextMask()
        {
            currentMaskIndex++;

            if (currentMaskIndex >= totalMasks)
                currentMaskIndex = 0;

            spriteTexture.SetData(textureColorSequence[currentMaskIndex]);
        }

        public void Reset()
        {
            currentMaskIndex = 0;
            spriteTexture.SetData(textureColorSequence[currentMaskIndex]);
        }
    }
}
