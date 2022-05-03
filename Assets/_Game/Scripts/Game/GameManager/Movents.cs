using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;
using JovDK.Debug;

using WebPong.Models.WebSocketEvents;

public partial class GameManager : MonoBehaviour
{

    public void UpdateStates()
    {

        SendLocalPlayerPosition();

        if (_ball.IsInLocalControl())
            SendBallPosition();

    }

    public void SendBallState()
    {

        _networkManager.SendBallState(
            _ball.CurrentControllerPlayerIndex,
            _ball.DestinationPosition,
            _ball.CurrentRotation,
            _ball.CurrentVelocity);

    }

    public void ApplyBallState(int currentControllerPlayerIndex, Vector3 destinationPosition, Vector3 currentRotation, float currentVelocity)
    {

        _ball.ApplyState(
            currentControllerPlayerIndex,
            destinationPosition,
            currentRotation,
            currentVelocity);

    }

    void SendBallPosition()
    {

        _networkManager.SendBallPosition(
            _ball.DestinationPosition,
            _ball.CurrentRotation,
            _ball.CurrentVelocity);

    }

    public void ApplyBallPosition(Vector3 destinationPosition, Vector3 currentRotation, float currentVelocity)
    {

        if (!_ball.IsInLocalControl())
            _ball.ApplyPosition(
                destinationPosition,
                currentRotation,
                currentVelocity);

    }

    void SendLocalPlayerPosition()
    {

        _networkManager.SendPlayerPosition(_localPlayerIndex, GetLocalPlayer()._destinationPosition);

    }

    public void ApplyPlayerPosition(int playerIndex, float destinationPosition)
    {

        // aplly network player movement
        if (playerIndex >= 0 && playerIndex != _localPlayerIndex)
        {

            GetPlayerByIndex(playerIndex).SetDestinationPosition(destinationPosition);

        }

    }

    public void ApplyLocalPlayerMovementInput(float movementInput)
    {

        Player localPlayer = GetLocalPlayer();

        localPlayer.DoIfNotNull(() =>
        {

            // localPlayer.position = Mathf.Clamp(localPlayer.position + (movementInput * maximumPlayersInputVelocity * Time.deltaTime), -1f, 1f);


            // Vector3 newPlayerPosition;

            // if (_localPlayerPosition == 0)
            //     newPlayerPosition = new Vector3(localPlayer.position * maximumPlayersDistance,
            //         localPlayer.transform.position.y,
            //         localPlayer.transform.position.z);
            // else
            //     newPlayerPosition = new Vector3(-localPlayer.position * maximumPlayersDistance,
            //         localPlayer.transform.position.y,
            //         localPlayer.transform.position.z);

            // localPlayer.transform.position = newPlayerPosition;

            localPlayer.ApplyMovementInput(movementInput);

            // localPlayer.playerView.ApplyVelocity(movementInput);

        });

    }



}
