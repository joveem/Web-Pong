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

    void SetupMatch(object eventDataObject)
    {

        SetupMatchEventData eventData = ConvertEventObject<SetupMatchEventData>(eventDataObject);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.HidePanel();

        });

        GameManager.instance.SetupMatch(eventData.PlayerIndex, eventData.CurrentPlayerIndex);

    }

    void StartRound(object eventDataObject)
    {

        StartMatchEventData eventData = ConvertEventObject<StartMatchEventData>(eventDataObject);

        GameManager.instance.StartRound(eventData.CurrentPlayerIndex);

    }

    void ShowMatchResultWin(object eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventObject<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowWinAlert(eventData.PlayersScores);

    }

    void ShowMatchResultLose(object eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventObject<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowLoseAlert(eventData.PlayersScores);

    }
    void ShowMatchResultWinByGiveUp(object eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventObject<ScoreUpdateEventData>(eventDataObject);

        GameManager.instance.MatchMenuPanel.ShowWinAlert(eventData.PlayersScores, true);

    }

}
