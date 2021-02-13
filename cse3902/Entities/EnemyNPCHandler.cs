using System;
using System.Collections.Generic;
using cse3902.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Items
{
    public class EnemyNPCHandler
    {

        private List<IEntity> enpcs;
        private Game1 game;
        private int enpcIndex;
        private int positionInSpriteList;


        public EnemyNPCHandler(Game1 thegame)
        {
            enpcs = new List<IEntity>();
            enpcIndex = 0;
            game = thegame;
            positionInSpriteList = game.spriteList.Count;
            LoadEntities();
        }

        private void LoadEntities()
        {
            enpcs.Add(new OldManNPC(game));
            enpcs.Add(new MedicineWomanNPC(game));
            enpcs.Add(new MerchantNPC(game));
        }

        public void Update(GameTime gameTime)
        {
            enpcs[enpcIndex].Update(gameTime);
        }

        public void Draw()
        {
            enpcs[enpcIndex].Draw();
        }

        public void displayNext()
        {
            enpcIndex++;
            if (enpcIndex == enpcs.Count) enpcIndex = 0;
        }

        public void displayPrev()
        {
            enpcIndex--;
            if (enpcIndex == -1) enpcIndex = enpcs.Count - 1;
        }

    }
}
