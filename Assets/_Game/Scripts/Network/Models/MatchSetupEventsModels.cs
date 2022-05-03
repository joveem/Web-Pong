using System;
using UnityEngine;
using JovDK.Models.Network;

namespace WebPong.Models.WebSocketEvents
{


    [Serializable]
    public class SetupMatchEventData
    {

        public int PlayerIndex = -1;
        public int CurrentPlayerIndex = -1;

    }

    [Serializable]
    public class StartMatchEventData
    {

        public int CurrentPlayerIndex = -1;

    }

}
