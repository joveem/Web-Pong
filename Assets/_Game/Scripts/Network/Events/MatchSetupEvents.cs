using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

using WebPong.Models.WebSocketEvents;
public partial class NetworkManager
{

    void SetupMatch(dynamic eventDataObject)
    {

        SetupMatchEventData eventData = ConvertEventData<SetupMatchEventData>(eventDataObject);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.HidePanel();

        });

        GameManager.instance.SetupMatch(eventData.PlayerIndex, eventData.CurrentPlayerIndex);

    }

    void StartRound(dynamic eventDataObject)
    {

        StartMatchEventData eventData = ConvertEventData<StartMatchEventData>(eventDataObject);

        GameManager.instance.StartRound(eventData.CurrentPlayerIndex);

    }

    void ShowMatchResultWin(dynamic eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventData<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowWinAlert(eventData.PlayersScores);

    }

    void ShowMatchResultLose(dynamic eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventData<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowLoseAlert(eventData.PlayersScores);

    }
    void ShowMatchResultWinByGiveUp(dynamic eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventData<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowWinAlert(eventData.PlayersScores, true);

    }

}
