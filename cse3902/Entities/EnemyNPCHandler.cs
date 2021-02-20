﻿using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Entities.Enemies;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Items
{
    public class EnemyNPCHandler
    {

        private List<IEntity> enpcs;
        private Game1 game;
        private int enpcIndex;


        public EnemyNPCHandler(Game1 thegame)
        {
            enpcs = new List<IEntity>();
            enpcIndex = 0;
            game = thegame;
        }

        public void LoadContent()
        {
            NPCSpriteFactory.Instance.LoadAllTextures(game.Content);

            enpcs.Add(new OldManNPC(game));
            enpcs.Add(new MedicineWomanNPC(game));
            enpcs.Add(new MerchantNPC(game));
            enpcs.Add(new Aquamentus(game));
            enpcs.Add(new Gel(game));
            enpcs.Add(new Goriya(game));
            enpcs.Add(new Keese(game));
            enpcs.Add(new Stalfos(game));
            enpcs.Add(new WallMaster(game));
        }

        public void Update(GameTime gameTime)
        {
            enpcs[enpcIndex].Update(gameTime);
        }

        public void Draw()
        {
            enpcs[enpcIndex].Draw();
        }

        public void cycleNext()
        {
            enpcIndex++;
            if (enpcIndex == enpcs.Count) enpcIndex = 0;
        }

        public void cyclePrev()
        {
            enpcIndex--;
            if (enpcIndex == -1) enpcIndex = enpcs.Count - 1;
        }
        public void Reset()
        {
            enpcIndex = 0;
        }
    }
}
