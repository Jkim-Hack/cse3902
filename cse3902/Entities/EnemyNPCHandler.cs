using System.Collections.Generic;
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

        private int maxFrameTime;
        private int currentFrameTime; 

        public EnemyNPCHandler(Game1 thegame)
        {
            enpcs = new List<IEntity>();
            enpcIndex = 0;
            game = thegame;
            maxFrameTime = 10;
            currentFrameTime = 0;
        }

        private void InitializeEnemyNPC()
        {
            enpcs.Add(new OldManNPC(game));
            enpcs.Add(new MedicineWomanNPC(game));
            enpcs.Add(new MerchantNPC(game));

            //enemies would be included here, but enemy constructors load the content file
        }
        public void LoadContent()
        {
            NPCSpriteFactory.Instance.LoadAllTextures(game.Content);

            InitializeEnemyNPC();
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
            if (currentFrameTime < maxFrameTime) currentFrameTime++;
        }

        public void Draw()
        {
            enpcs[enpcIndex].Draw();
        }

        public void CycleNext()
        {
            if (currentFrameTime == maxFrameTime) { 
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
            enpcs[3] = new Aquamentus(game); //special case needed to reset his fireballs
            enpcIndex = 0;
        }
    }
}
