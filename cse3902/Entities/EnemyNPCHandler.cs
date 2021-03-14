using cse3902.Entities;
using cse3902.Entities.Enemies;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace cse3902.Items
{
    public class EnemyNPCHandler
    {
        private List<IEntity> enpcs;
        private Game1 game;
        private int enpcIndex;

        private int maxFrameTime;
        private int currentFrameTime;

        private Vector2 startPos;

        public EnemyNPCHandler(Game1 thegame)
        {
            enpcs = new List<IEntity>();
            enpcIndex = 0;
            game = thegame;
            maxFrameTime = 10;
            currentFrameTime = 0;
            startPos = new Vector2(100, 200);
        }

        private void InitializeEnemyNPC()
        {
            enpcs.Add(new OldManNPC(game, startPos));
            enpcs.Add(new MedicineWomanNPC(game, startPos));
            enpcs.Add(new MerchantNPC(game, startPos));

            //enemies would be included here, but enemy constructors load the content file
        }

        public void LoadContent()
        {
            NPCSpriteFactory.Instance.LoadAllTextures(game.Content);
            EnemySpriteFactory.Instance.LoadAllTextures(game.Content);

            InitializeEnemyNPC();
            enpcs.Add(new Aquamentus(game, startPos));
            enpcs.Add(new Gel(game, startPos));
            enpcs.Add(new Goriya(game, startPos));
            enpcs.Add(new Keese(game, startPos));
            enpcs.Add(new Stalfos(game, startPos));
            enpcs.Add(new WallMaster(game, startPos));
        }

        public void Update(GameTime gameTime)
        {
            enpcs[enpcIndex].Update(gameTime);
            if (currentFrameTime < maxFrameTime) currentFrameTime++;
        }

        public void Draw()
        {
            enpcs[enpcIndex].Draw();
        }

        public void CycleNext()
        {
            if (currentFrameTime == maxFrameTime)
            {
                enpcIndex++;
                if (enpcIndex == enpcs.Count) enpcIndex = 0;
                currentFrameTime = 0;
            }
        }

        public void CyclePrev()
        {
            if (currentFrameTime == maxFrameTime)
            {
                enpcIndex--;
                if (enpcIndex == -1) enpcIndex = enpcs.Count - 1;
                currentFrameTime = 0;
            }
        }

        public void Reset()
        {
            //the list should be remade, but enemy content would be reloaded so no change
            enpcIndex = 0;
        }
    }
}