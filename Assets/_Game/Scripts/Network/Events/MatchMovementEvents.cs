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

    async public void SendPlayerPosition(int playerIndex, float destinationPosition)
    {

        PlayerPositionEventData eventData = new PlayerPositionEventData(
                                                playerIndex,
                                                destinationPosition);

        await Send("match-update-player-position", eventData);

    }

    void ApplyPlayerPosition(object eventDataObject)
    {

        PlayerPositionEventData eventdata = ConvertEventObject<PlayerPositionEventData>(eventDataObject);

        _gameManager.ApplyPlayerPosition(eventdata.PlayerIndex, eventdata.DestinationPosition);

    }

    async public void SendBallState(int currentControllerPlayerIndex, Vector3 position, Vector3 eulerRotation, float velocity)
    {

        BallStateEventData eventData = new BallStateEventData(
                                        currentControllerPlayerIndex,
                                        position,
                                        eulerRotation,
                                        velocity);

        await Send("match-update-ball-state", eventData);

    }

    async public void SendBallPosition(Vector3 position, Vector3 eulerRotation, float velocity)
    {

        BallPositionEventData eventData = new BallPositionEventData(
                                        position,
                                        eulerRotation,
                                        velocity);

        await Send("match-update-ball-position", eventData);

    }

    void ApplyBallState(object eventDataObject)
    {

        DebugExtension.DevLogWarning("updating BallState!");

        BallStateEventData eventdata = ConvertEventObject<BallStateEventData>(eventDataObject);

        _gameManager.ApplyBallState(
            eventdata.CurrentControllerPlayerIndex,
            eventdata.Position.ToVector3(),
            eventdata.EulerRotation.ToVector3(),
            eventdata.Velocity);

    }
    void ApplyBallPosition(object eventDataObject)
    {

        BallPositionEventData eventdata = ConvertEventObject<BallPositionEventData>(eventDataObject);

        _gameManager.ApplyBallPosition(
            eventdata.Position.ToVector3(),
            eventdata.EulerRotation.ToVector3(),
            eventdata.Velocity);

    }

}
