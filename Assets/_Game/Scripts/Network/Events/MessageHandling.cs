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

    Dictionary<string, EventCallback> eventList = new Dictionary<string, EventCallback>();
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

    void SetEvent(string eventName, EventCallback eventCallback)
    {

        var callback = new EventCallback(eventCallback);

        eventList.Add(eventName, callback);

    }

    void HandleSocketEvent(SocketEvent socketEvent)
    {

        if (eventList.TryGetValue(socketEvent.EventName, out EventCallback callback))
            callback(socketEvent.EventData);
        else
            DebugExtension.DevLogWarning("UNEXPECTED EventName!".ToColor(GoodCollors.orange) +
                " ( EventName = " + socketEvent.EventName + ")");

    }

    public delegate void EventCallback(object eventDataObject);

}
