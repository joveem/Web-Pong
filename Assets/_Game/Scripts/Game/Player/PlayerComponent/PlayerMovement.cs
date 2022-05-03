using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;

public partial class Player : MonoBehaviour
{

    void DebugPositions()
    {

        Vector3 debugDestinationPosition = _initialPosition +
            (new Vector3(_destinationPosition * GameManager.instance.MaxPlayersDistance, 0, 0));

        Debug.DrawLine(debugDestinationPosition, debugDestinationPosition + Vector3.up * 5, new Color(1f, 0f, 1f));

        // Vector3 currentPosition = _initialPosition +
        //     (new Vector3(destination * GameManager.instance.MaxPlayersDistance, 0, 0));

        // Debug.DrawLine(debugDestinationPosition, debugDestinationPosition + Vector3.up * 5, Color.red);

    }

    void ApplyPosition()
    {


        if (_rigidbody != null)
        {

            float targetMovementPositionOffset = _destinationPosition - _currentPosition;
            float maxPlayerVelocity = GameManager.instance.MaxPlayersInputVelocity;
            float deltaMaxPlayerVelocity = Time.deltaTime * maxPlayerVelocity;
            float MaxPlayersDistance = GameManager.instance.MaxPlayersDistance;

            float finalPositionOffset;
            if (IsLocalPlayer())
            {

                finalPositionOffset = Mathf.Clamp(
                                            targetMovementPositionOffset,
                                            -deltaMaxPlayerVelocity,
                                            deltaMaxPlayerVelocity);

            }
            else
            {

                targetMovementPositionOffset *= Time.deltaTime * maxPlayerVelocity;

                finalPositionOffset = Mathf.Clamp(
                                            targetMovementPositionOffset,
                                            -deltaMaxPlayerVelocity,
                                            deltaMaxPlayerVelocity);

            }

            _currentPosition += finalPositionOffset;

            Vector3 convertedFinalMovement = _initialPosition +
                                                new Vector3(_currentPosition, 0f, 0f) *
                                                MaxPlayersDistance;

            _rigidbody.MovePosition(convertedFinalMovement);

            _currentVelocity = finalPositionOffset / deltaMaxPlayerVelocity;
            _playerView.SetVelocity(_currentVelocity);

        }

    }




    public void ApplyMovementInput(float movementInput)
    {

        if (invertInput)
            movementInput = -movementInput;

        float MaxPlayerVelocity = GameManager.instance.MaxPlayersInputVelocity;
        _destinationPosition = _destinationPosition + movementInput * MaxPlayerVelocity * Time.deltaTime;
        _destinationPosition = Mathf.Clamp(_destinationPosition, -1f, 1f);

    }

    public void SetDestinationPosition(float destinationPosition)
    {

        _destinationPosition = destinationPosition;

    }

}
