using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

public partial class Ball : MonoBehaviour
{

    public void SetLocalPlayerIndex(int localPlayerIndex)
    {

        _localPlayerIndex = localPlayerIndex;

    }

    public void ResetPosition()
    {

        Vector3 initialPosition = Vector3.zero;
        Vector3 initialRotation = new Vector3(0f, 180f, 0f);

        _currentPosition = initialPosition;
        _destinationPosition = initialPosition;
        transform.position = initialPosition;

        _currentRotation = initialRotation;
        transform.rotation = Quaternion.Euler(initialRotation);

    }

    public void ResetPosition(int controllerPlayerIndex)
    {

        Vector3 initialPosition = Vector3.zero;
        Vector3 initialRotation =
            controllerPlayerIndex == 0 ?
            new Vector3(0f, 180f, 0f) :
            new Vector3(0f, 0f, 0f);

        _currentPosition = initialPosition;
        _destinationPosition = initialPosition;
        transform.position = initialPosition;

        _currentRotation = initialRotation;
        transform.rotation = Quaternion.Euler(initialRotation);

    }

    public void ShowBallWithDrop()
    {

        _isActive = true;
        _ballView.DoIfNotNull(() =>
            _ballView.ShowBallWithDrop());

    }

    public void DisableBall()
    {

        _isActive = false;
        HideBall();

    }

    public void HideBall()
    {

        _ballView.DoIfNotNull(() =>
            _ballView.HideBall());

    }

    public void SetControllerPlayerIndex(int currentPlayerIndex)
    {

        _currentControllerPlayerIndex = currentPlayerIndex;

    }

    void SwitchPlayer()
    {

        switch (_currentControllerPlayerIndex)
        {

            case 0:
                _currentControllerPlayerIndex = 1;
                DebugExtension.DevLog("Switching player id! " +
                    "( from " + "0".ToColor(GoodCollors.red) + " to " +
                    "1".ToColor(GoodCollors.green) + " )");
                break;

            case 1:
                DebugExtension.DevLog("Switching player id! " +
                    "( from " + "1".ToColor(GoodCollors.red) + " to " +
                    "0".ToColor(GoodCollors.green) + " )");
                _currentControllerPlayerIndex = 0;
                break;

            default:
                DebugExtension.DevLogWarning("UNEXPECTED player id! ( CurrentControllerPlayerIndex = " + _currentControllerPlayerIndex + " )");
                return;

        }

        GameManager.instance.SendBallState();

    }

    public void RestartComponent()
    {

        _isFirstRoundHit = true;

    }

}
