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

        public void AddTrap(ITrap trap)
        {
            traps.Add(trap);
        }

        public void RemoveTrap(ITrap trap)
        {
            (traps as List<ITrap>).RemoveAll(x => x.Center == trap.Center);
        }

        public void Update(GameTime gameTime)
        {
            foreach (ITrap trap in traps)
            {
                trap.Update(gameTime);
            }
        }

        public void Draw()
        {

            foreach (ITrap trap in traps)
            {
                trap.Draw();
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