using cse3902.Entities.DamageMasks;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace cse3902.Rooms
{
    public class DungeonMask
    {

        private List<GenericTextureMask> maskList;
        private List<Texture2D> textureList;

        private Texture2D damageSequenceTexture;
        private int sequenceRows;
        private int sequenceCols;
        private int startingColorIndex;
        private bool isDisabled;

        private static DungeonMask instance = new DungeonMask();
        public static DungeonMask Instance
        {
            get
            {
                return instance;
            }
        }
        private DungeonMask()
        {
            maskList = new List<GenericTextureMask>();
            textureList = new List<Texture2D>();
            isDisabled = false;
            sequenceRows = 0;
            sequenceCols = 0;
        }
        public void setMaskTexture(Texture2D damageSequenceTexture, int sequenceRows, int sequenceColumns, int startingColorIndex)
        {
            this.damageSequenceTexture = damageSequenceTexture;
            this.sequenceRows = sequenceRows;
            this.sequenceCols = sequenceColumns;
            this.startingColorIndex = startingColorIndex;

            this.Reset();
            this.maskList.Clear();


            foreach(Texture2D texture in textureList)
            {
                createMask(texture);
            }

        }
        private void createMask(Texture2D spriteTexture)
        {
            maskList.Add(new GenericTextureMask(spriteTexture, damageSequenceTexture, sequenceRows, sequenceCols, startingColorIndex));
        }

        public void addTexture(Texture2D spriteTexture)
        {
            textureList.Add(spriteTexture);
            if(damageSequenceTexture != null)
            {
                createMask(spriteTexture);
            }
        }

        public void LoadNextMask(int maskIndex)
        {
            if (isDisabled) return;
            foreach (GenericTextureMask mask in maskList) {
                mask.changeMask(maskIndex);
            }
        }

        public void Reset()
        {
            foreach (GenericTextureMask mask in maskList)
            {
                mask.Reset();
            }
        }

        public bool Disabled
        {
            get => isDisabled;
            set => isDisabled = value;
        }

        public int MaskCount
        {
            get => sequenceCols * sequenceRows;
        }
    }
}