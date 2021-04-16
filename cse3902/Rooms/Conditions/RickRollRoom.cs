﻿using cse3902.Interfaces;
using cse3902.Sounds;

namespace cse3902.Rooms.Conditions
{
    public class RickRollRoom : ICondition
    {
        public RickRollRoom()
        {
        }

        public void CheckCondition()
        {
        }

        public void SendSignal()
        {

        }

        public void Reset()
        {

        }

        public void EnterRoom()
        {
            SoundFactory.Instance.backgroundMusic.Stop();
            //rickrollsound.play
        }

        public void LeaveRoom()
        {
            SoundFactory.Instance.backgroundMusic.Play();
            //rickrollsound.stop
        }
    }
}
