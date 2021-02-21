using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class NPCSpriteFactory : ISpriteFactory
    {
        private Dictionary<string, Texture2D> npcTextures;

        private static NPCSpriteFactory npcSpriteFactoryInstance = new NPCSpriteFactory();

        public static NPCSpriteFactory Instance
        {
            get => npcSpriteFactoryInstance;
        }

        public NPCSpriteFactory()
        {
            npcTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            npcTextures.Add("oldman", content.Load<Texture2D>("oldman"));
            npcTextures.Add("medicinewoman", content.Load<Texture2D>("medicinewoman"));
            npcTextures.Add("merchant", content.Load<Texture2D>("merchant"));
        }

        public ISprite CreateOldManSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["oldman"], startingPos);
        }

        public ISprite CreateMedicineWomanSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["medicinewoman"], startingPos);
        }
        
        public ISprite CreateMerchantSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["merchant"], startingPos);
        }
    }
}
