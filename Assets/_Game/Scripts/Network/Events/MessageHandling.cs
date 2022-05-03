using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using NativeWebSocket;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

using WebPong.Models.WebSocketEvents;

public partial class NetworkManager
{

    Dictionary<string, Action<dynamic>> eventList = new Dictionary<string, Action<dynamic>>();
    void SetupEvents()
    {

        // ----- Main Menu events -----
        SetEvent("enter-queue-result", EnteredQueueResponse);
        SetEvent("exit-queue-result", ExitedQueueResponse);

        // ----- Main Menu events -----
        SetEvent("setup-match", SetupMatch);
        SetEvent("match-result-win", ShowMatchResultWin);
        SetEvent("match-result-lose", ShowMatchResultLose);
        SetEvent("match-result-win-by-give-up", ShowMatchResultWinByGiveUp);

        // ----- Match events -----
        ////////////////////////////////////////
        SetEvent("match-start-round", StartRound);
        SetEvent("match-score-update", ApplyScore);
        SetEvent("match-ball-explosion", ApplyExplosion);

        // ----- Match Movements events -----
        SetEvent("match-update-player-position", ApplyPlayerPosition);
        SetEvent("match-update-ball-state", ApplyBallState);
        SetEvent("match-update-ball-position", ApplyBallPosition);

    }


    void SetEvent(string eventName, Action<dynamic> callback)
    {

        eventList.Add(eventName, callback);

    }

    void HandleSocketEvent(SocketEvent socketEvent)
    {

        if (eventList.TryGetValue(socketEvent.EventName, out Action<dynamic> callback))
            callback(socketEvent.EventData);
        else
            DebugExtension.DevLogWarning("UNEXPECTED EventName!".ToColor(GoodCollors.orange) +
                " ( EventName = " + socketEvent.EventName + ")");

    }

}
