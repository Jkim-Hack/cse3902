﻿using System.Collections.Generic;

namespace cse3902.Interfaces
{
    public interface ICondition
    {
        public void CheckCondition();
        public void SendSignal();
        public void Reset();
        public void EnterRoom();
        public void LeaveRoom();

    }
}
