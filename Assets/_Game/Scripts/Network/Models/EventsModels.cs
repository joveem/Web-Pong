using System;

namespace WebPong.Models.WebSocketEvents
{

    [Serializable]
    public class SocketEvent
    {

        public string EventName = "UNDEFINED";
        public object EventData = null;

    }



}
