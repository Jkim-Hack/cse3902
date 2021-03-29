using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Entities;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomTraps
    {
        public IList traps;

        private static RoomTraps instance = new RoomTraps();

        public static RoomTraps Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomTraps()
        {
            traps = new List<ITrap>();
        }

        public void AddEnemy(ITrap enemy)
        {
            traps.Add(enemy);
        }

        public void RemoveEnemy(ITrap enemy)
        {
            (traps as List<ITrap>).RemoveAll(x => x.Center == enemy.Center);
        }

        public void Update(GameTime gameTime)
        {
            if (CloudAnimation.Instance.cloudAnims.Count == 0)
            {
                foreach (ITrap trap in traps)
                {
                    trap.Update(gameTime);
                }
            }
        }

        public void Draw()
        {
            if (CloudAnimation.Instance.cloudAnims.Count == 0)
            {
                foreach (ITrap trap in traps)
                {
                    trap.Draw();
                }
            }
        }

        public void LoadNewRoom(ref List<ITrap> oldList, List<ITrap> newList, Game1 game)
        {
            oldList = new List<ITrap>();

            List<ITrap> traptemp = traps as List<ITrap>;

            for (int i = 0; i < traps.Count; i++)
            {
                oldList.Add(traptemp[i]);
            }

            traps.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                traps.Add(newList[i]);
            }

            //todo: don't think traps need the cloud animation, make sure though
            //CloudAnimation.Instance.LoadNewRoom(newList, game.SpriteBatch);
        }

        public ref IList ListRef
        {
            get => ref traps;
        }

    }
}