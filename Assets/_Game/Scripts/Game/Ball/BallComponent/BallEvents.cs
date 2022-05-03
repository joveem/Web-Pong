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

    public void ApplyState(
        int currentControllerPlayerIndex,
        Vector3 destinationPosition,
        Vector3 currentRotation,
        float currentVelocity)
    {

        ////////////////////////////
        // Debug.LogWarning("_currentControllerPlayerIndex".ToColor(GoodCollors.orange) +
        //     " = " + _currentControllerPlayerIndex);
        SetControllerPlayerIndex(currentControllerPlayerIndex);
        // Debug.LogWarning("_currentControllerPlayerIndex".ToColor(GoodCollors.green) +
        //     " = " + _currentControllerPlayerIndex);

        _destinationPosition = destinationPosition;
        _currentRotation = currentRotation;
        _currentVelocity = currentVelocity;

        _isFirstRoundHit = false;

    }

    public void ApplyPosition(
        Vector3 destinationPosition,
        Vector3 currentRotation,
        float currentVelocity)
    {

        _destinationPosition = destinationPosition;
        _currentRotation = currentRotation;
        _currentVelocity = currentVelocity;

    }

}
