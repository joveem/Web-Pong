using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;

public partial class Player : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] new Rigidbody _rigidbody;


    [Space(5), Header("[ State ]"), Space(10)]

    public int Index;
    [SerializeField] float _currentPosition;
    [SerializeField] public float _destinationPosition;
    public float DestinationPosition { get => _destinationPosition; }
    [SerializeField] float _currentVelocity;
    [SerializeField] float _latency;


    [Space(5), Header("[ Parts ]"), Space(10)]

    public GameObject playerMesh;
    [SerializeField] public PlayerView _playerView;


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] bool invertInput = false;
    [SerializeField] public Vector3 _initialPosition;



    void Start()
    {

        SetupPlayer();

    }

    void Update()
    {

        DebugPositions();

    }

 

    void FixedUpdate()
    {

        HandleMovement();

    }

    void HandleMovement()
    {

        ApplyPosition();

    }
    

    public bool IsLocalPlayer()
    {

        return Index == GameManager.instance.LocalPlayerIndex;

    }
}
