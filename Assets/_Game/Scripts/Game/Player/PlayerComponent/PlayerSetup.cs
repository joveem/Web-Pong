using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;

public partial class Player : MonoBehaviour
{

    void SetupPlayer()
    {

        _playerView.DoIfNull(() => SafeActionsTools.TryGetComponent(this, out _playerView), false);
        _rigidbody.DoIfNull(() => SafeActionsTools.TryGetComponent(this, out _rigidbody), false);

        _initialPosition = _rigidbody.position;

    }

    public void ResetPosition()
    {

        transform.position = _initialPosition;

        _currentVelocity = 0f;

        _currentPosition = 0f;
        _destinationPosition = 0f;

    }

    public void DisablePlayer()
    {

        Index = -1;
        ResetPosition();

    }

}
