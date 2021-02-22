﻿using System.Collections.Generic;
using cse3902.Entities;
using cse3902.Sprites;
using cse3902.Entities.Enemies;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Items
{
    public class EnemyNPCHandler
    {

        private List<IEntity> enpcs;
        private Game1 game;
        private int enpcIndex;

        private int maxframetime;
        private int currentframetime;

        private FireballSprite fireball;
 

        public EnemyNPCHandler(Game1 thegame)
        {
            enpcs = new List<IEntity>();
            enpcIndex = 0;
            game = thegame;
            maxframetime = 10;
            currentframetime = 0;

          
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
            if (currentframetime < maxframetime) currentframetime++;
        }

        public void Draw()
        {
            //if (enpcIndex == 3)
            //{
            //    fireball = new FireballSprite(game.spriteBatch, game.Content.Load<Texture2D>("fireball"), new Vector2(100, 250), new Vector2(1, 0));
            //    fireball.Draw();

            //}
            enpcs[enpcIndex].Draw();
        }

        public void cycleNext()
        {
            if (currentframetime == maxframetime) { 
                enpcIndex++;
                if (enpcIndex == enpcs.Count) enpcIndex = 0;
                currentframetime = 0;
            }
        }

        public void cyclePrev()
        {
            if (currentframetime == maxframetime)
            {
                enpcIndex--;
                if (enpcIndex == -1) enpcIndex = enpcs.Count - 1;
                currentframetime = 0;
            }
        }
        public void Reset()
        {
            enpcIndex = 0;
        }
    }
}
