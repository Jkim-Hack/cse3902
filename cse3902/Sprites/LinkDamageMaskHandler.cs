using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class LinkDamageMaskHandler
    {
        private Texture2D linkTexture;
        private List<Color[]> textureColorSequence;

        private Color[] rawData;
        private int dataSize;

        private Color[,] linkColors;
        private const int totalLinkColors = 3;

        private const int totalMasks = 4;

        private int currentMaskIndex;

        public LinkDamageMaskHandler(Texture2D linkTexture)
        {
            this.linkTexture = linkTexture;
            
	        dataSize = linkTexture.Width * linkTexture.Height;
	        rawData = new Color[dataSize];
            linkTexture.GetData(rawData);

            linkColors = new Color[totalMasks, totalLinkColors];
            GetAllLinkColors();

	        textureColorSequence = new List<Color[]>();
            textureColorSequence.Add(rawData);
            textureColorSequence.Add(new Color[dataSize]);
            textureColorSequence.Add(new Color[dataSize]);
            textureColorSequence.Add(new Color[dataSize]);

            GenerateTextureMasks();
            currentMaskIndex = 0;
        }

        private void GetAllLinkColors()
        {    
            // Skin
	        linkColors[0,0] = new Color(252, 152, 56, 255);
            // Green Clothes
            linkColors[0,1] = new Color(128, 208, 16, 255);
            // Arms, legs, belt, hair
            linkColors[0,2] = new Color(200, 76, 12, 255);
	        
	        linkColors[1,0] = new Color(0, 128, 136, 255);
            linkColors[1,1] = new Color(0, 0, 0, 255);
            linkColors[1,2] = new Color(216, 40, 0, 255);
	        
	        linkColors[2,0] = new Color(252, 152, 56, 255);
            linkColors[2,1] = new Color(216, 40, 0, 255);
            linkColors[2,2] = new Color(252, 252, 252, 255);
	        
	        linkColors[3,0] = new Color(92, 148, 252, 255);
            linkColors[3,1] = new Color(0, 0, 168, 255);
            linkColors[3,2] = new Color(252, 252, 252, 255);
        }

        private void GenerateTextureMasks()
        {
	        for (int j = 0; j < rawData.Length; j++)
	        {
		        for (int k = 0; k < totalLinkColors; k++) 
			    {
		            for (int i = 1; i < totalMasks; i++)
		            {
					    if (rawData[j] == linkColors[0, k])
					    {
			                textureColorSequence[i][j] = linkColors[i, k];
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
            
	        Console.WriteLine(textureColorSequence[currentMaskIndex][150]);
	        linkTexture.SetData(textureColorSequence[currentMaskIndex]);
        }

        public void Reset()
        {
            currentMaskIndex = 0;
            linkTexture.SetData(textureColorSequence[currentMaskIndex]);
        }

    }
}
