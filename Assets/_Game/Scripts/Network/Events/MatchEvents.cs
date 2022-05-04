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

    public async void SendScoring(
        int scoringPlayerIndex)
    {

        ScoringRequestEventData scoringEventData = new ScoringRequestEventData(scoringPlayerIndex);

        await Send("match-register-scoring", scoringEventData);

    }

    public void ApplyScore(object eventDataObject)
    {

        ScoreUpdateEventData eventData = ConvertEventObject<ScoreUpdateEventData>(eventDataObject);

        _gameManager.ApplyScore(eventData.PlayersScores);

    }

    public async void SendExplosion(
        Vector3 explosionPosition,
        Quaternion explosionRotation,
        Vector3 explosionScale,
        int senderPlayerIndex)
    {

        int receiverPlayerIndex = -1;

        switch (senderPlayerIndex)
        {

            case 0:

                receiverPlayerIndex = 1;
                break;

            case 1:

                receiverPlayerIndex = 0;
                break;

            default:
                {

                    string debugText =
                        "UNEXPECTED sender player index! " +
                        "( senderPlayerIndex = " + senderPlayerIndex + " )";

                    DebugExtension.DevLogError(debugText);

                    break;

                }

        }

        if (receiverPlayerIndex != -1)
        {

            BallExplosionEventData ballExplosionEventData = new BallExplosionEventData(
                                                                explosionPosition,
                                                                explosionRotation,
                                                                explosionScale,
                                                                receiverPlayerIndex);

            await Send("match-ball-explosion", ballExplosionEventData);

        }


    }

    public void ApplyExplosion(object eventDataObject)
    {

        BallExplosionEventData eventData = ConvertEventObject<BallExplosionEventData>(eventDataObject);

        _gameManager.ApplyExplosion(
            eventData.Position.ToVector3(),
            Quaternion.Euler(eventData.EulerRotation.ToVector3()),
            eventData.Scale.ToVector3());

    }

    public async void SendPing(int pingId)
    {

        await Send("match-ping", pingId);

    }

    public void ApplyPing(object eventDataObject)
    {

        int eventData = int.Parse(eventDataObject.ToString());

        _gameManager.ApplyPing(eventData);

    }

}
