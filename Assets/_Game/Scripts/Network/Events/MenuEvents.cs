using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using WebSocketSharp;
using NativeWebSocket;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

using WebPong.Models.WebSocketEvents;

public partial class NetworkManager
{

    async void PlayButton()
    {

        DebugExtension.DevLog("Seaching match...");

        var socketEvent = new SocketEvent { EventName = "enter-match-queue" };


        _mainMenuPanel.ShowCancelButton();
        _mainMenuPanel.DisableButtons();

        SendEnterMatchQueueRequest();

    }

    async void SendEnterMatchQueueRequest()
    {

        await Send("enter-match-queue", null);

    }

    async void CancelButton()
    {

        DebugExtension.DevLog("Exiting match queue...");

        var socketEvent = new SocketEvent { EventName = "exit-match-queue" };

        _mainMenuPanel.ShowPlayButton();
        _mainMenuPanel.DisableButtons();

        SendExitMatchQueueRequest();

    }

    async void SendExitMatchQueueRequest()
    {

        await Send("exit-match-queue", null);

    }

    void EnteredQueueResponse(object eventDataObject)
    {

        QueueEventData eventData = ConvertEventObject<QueueEventData>(eventDataObject);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            if (eventData.Success)
            {

                DebugExtension.DevLog("Entered match queue Success!");

                _mainMenuPanel.ShowCancelButton();
                _mainMenuPanel.SetStatusText("Searching for match...".ToColor(GoodCollors.green));

            }
            else
            {

                DebugExtension.DevLogError("ERROR in match search!");

                _mainMenuPanel.ShowPlayButton();
                _mainMenuPanel.SetStatusText("ERROR in match search!".ToColor(GoodCollors.orange));

            }

            _mainMenuPanel.EnableButtons();

        });

    }

    void ExitedQueueResponse(object eventDataObject)
    {

        QueueEventData eventData = ConvertEventObject<QueueEventData>(eventDataObject);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            if (eventData.Success)
            {

                DebugExtension.DevLog("Exited match queue!");

                _mainMenuPanel.ShowPlayButton();
                _mainMenuPanel.SetStatusText("");

            }
            else
            {

                DebugExtension.DevLogError("ERROR in match search!");

                _mainMenuPanel.ShowCancelButton();
                _mainMenuPanel.SetStatusText("ERROR in match search!".ToColor(GoodCollors.orange));

            }

            _mainMenuPanel.EnableButtons();

        });

    }


}
