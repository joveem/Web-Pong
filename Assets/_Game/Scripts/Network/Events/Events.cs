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

    void Connect()
    {

        DebugExtension.DevLog("--- Connecting to the server...".ToColor(GoodCollors.orange));

        _webSocketClient.Connect();

        // mainMenuPanel.DoIfNotNull(() =>
        //     mainMenuPanel.SetStatusText("Connecting...".ToColor(GoodCollors.yellow)));

        _mainMenuPanel.DoIfNotNull(() =>
            _mainMenuPanel.SetStatusText("Connectiong...".ToColor(GoodCollors.yellow))
        );

    }

    void OnConnected()
    {

        DebugExtension.DevLog("--- CONNECTED to the server!".ToColor(GoodCollors.green));

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.ShowPlayButton();
            _mainMenuPanel.EnableButtons();
            _mainMenuPanel.SetStatusText("");

        });

    }

    void OnError(string message)
    {

        DebugExtension.DevLogError(message);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.ShowPlayButton();
            _mainMenuPanel.DisableButtons();
            _mainMenuPanel.SetStatusText("error...".ToColor(GoodCollors.orange));

        });

    }

    void OnClose(WebSocketCloseCode closeCode)
    {

        DebugExtension.DevLog(("--- DISCONNECTED ( closeCode = " + closeCode.ToString() + " )" + "---").ToColor(GoodCollors.red));

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.ShowPlayButton();
            _mainMenuPanel.ShowCancelButton();
            _mainMenuPanel.DisableButtons();
            _mainMenuPanel.SetStatusText(("DISCONNECTED from the server! (E91" + (int)closeCode + ")").ToColor(GoodCollors.orange));

        });


    }

    void OnMessage(byte[] message)
    {

        SocketEvent socketEvent = message.DeserializeBsonToObject<SocketEvent>();

        bool haveToShowDebug =
            socketEvent.EventName != "match-update-player-position" &&
            socketEvent.EventName != "match-update-ball-position";

        if (haveToShowDebug)
        {

            string debugText = "[ > Received ]".ToColor(GoodCollors.green) +
                " EventData = " + socketEvent.SerializeObjectToJSON();

            DebugExtension.DevLog(debugText);

        }

        HandleSocketEvent(socketEvent);

    }

    void OnEvent(string EventName, object EventData)
    {

        switch (EventName)
        {

            case "playerMovement":
                {

                    break;

                }
            default:
                {

                    DebugExtension.DevLogWarning("event NOT FOUND! ( EventName = " + EventName + " )");
                    break;

                }

        }

    }


    async Task Send(string eventName, object eventData = null)
    {


        SocketEvent socketEvent = new SocketEvent
        {

            EventName = eventName,
            EventData = eventData,

        };

        bool haveToShowDebug =
            eventName != "match-update-player-position" &&
            eventName != "match-update-ball-position";

        if (haveToShowDebug)
        {

            string debugText = "[ < Sending ]".ToColor(GoodCollors.orange) +
                " EventData = " + socketEvent.SerializeObjectToJSON();

            DebugExtension.DevLog(debugText);

        }

        await _webSocketClient.Send(socketEvent.SerializeObjectToBSON());

    }

    T ConvertEventData<T>(dynamic dynamicEventDataObject)
    {

        return JsonSerializingTools.ConvertDynamicJsonToObject<T>(dynamicEventDataObject);

    }

}
