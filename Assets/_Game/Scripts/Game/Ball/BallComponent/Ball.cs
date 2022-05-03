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

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] BallView _ballView;
    [SerializeField] Rigidbody _rigidbody;


    [Space(5), Header("[ State ]"), Space(10)]

    [SerializeField] bool _isActive = false;
    [SerializeField] int _localPlayerIndex = -1;
    [SerializeField] int _currentControllerPlayerIndex = -1;
    [SerializeField] bool _isInLocalControl;
    [SerializeField] public int CurrentControllerPlayerIndex { get => _currentControllerPlayerIndex; }
    // [SerializeField] public int CurrentControllerPlayerIndex { get => _currentControllerPlayerIndex; }
    [SerializeField] bool _isFirstRoundHit = true;
    [SerializeField] Vector3 _currentPosition;
    public Vector3 CurrentPosition { get => _currentPosition; }
    [SerializeField] Vector3 _destinationPosition;
    public Vector3 DestinationPosition { get => _destinationPosition; }
    [SerializeField] Vector3 _currentRotation;
    public Vector3 CurrentRotation { get => _currentRotation; }
    [SerializeField] float _currentVelocity;
    [SerializeField] public float CurrentVelocity { get => _currentVelocity; }


    // [Space(5), Header("[ Parts ]"), Space(10)]



    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] float _maxRebounceAngle = 60f;
    [SerializeField] const float networkMovementSensibility = 0.8f;
    [SerializeField] float _maxRebounceDistance = 3.3f;


    [SerializeField]
    Vector3[] _playersRebounceBaseRotation = new Vector3[] {
                                                new Vector3(0f, 0f, 0f),
                                                new Vector3(0f, 180f, 0f)
                                            };


    void Awake()
    {



    }

    void Start()
    {

        SetupComponent();

    }

    void SetupComponent()
    {

        _rigidbody.DoIfNull(() =>
            SafeActionsTools.TryGetComponent(this, out _rigidbody), false);
        _ballView.DoIfNull(() =>
            SafeActionsTools.TryGetComponent(this, out _ballView), false);

    }

    void Update()
    {

        _isInLocalControl = IsInLocalControl();

    }

    void FixedUpdate()
    {

        HandleMovement();

    }


    public bool IsInLocalControl()
    {

        return _localPlayerIndex >= 0 && _localPlayerIndex == _currentControllerPlayerIndex;

    }

}
