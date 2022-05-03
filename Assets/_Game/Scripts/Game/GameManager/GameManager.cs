using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;
using JovDK.Debug;

public partial class GameManager : MonoBehaviour
{

    public static GameManager instance;

    GameManager()
    {

        if (instance == null)
            instance = this;
        else
            DebugExtension.DevLogError("Two or more GameManager instance WAS FOUND!");

    }

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] MainMenuPanel _mainMenuPanel;
    [SerializeField] NetworkManager _networkManager;
    [SerializeField] MatchScorePanel _matchScorePanel;
    public MatchScorePanel MatchScorePanel { get => _matchScorePanel; }
    [SerializeField] MatchMenuPanel _matchMenuPanel;
    public MatchMenuPanel MatchMenuPanel { get => _matchMenuPanel; }
    [SerializeField] InputManager _inputManager;


    [Space(5), Header("[ State ]"), Space(10)]

    [SerializeField] public bool IsMatchStarted = false;
    [SerializeField] int _localPlayerIndex = -1;
    public int LocalPlayerIndex { get => _localPlayerIndex; }


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Camera mainCamera;
    [SerializeField] Transform mainCameraPivot;
    [SerializeField] List<Player> _playersList = new List<Player>();
    [SerializeField] Ball _ball;


    [Space(5), Header("[ Configs ]"), Space(10)]

    public float MaxPlayersInputVelocity = 2f;
    public float MaxPlayersDistance = 7f;
    public float MaxHitDistance = 3.3f;
    public float MaxVelocity = 2f;
    public float MaxBallVelocity = 5f;
    [SerializeField] ParticleSystem _ballExplosionParticlePrefab;


    void Start()
    {

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.ShowPanel();

        });

    }

    void FixedUpdate()
    {

        if (IsMatchStarted)
            UpdateStates();

    }

    public Player GetLocalPlayer()
    {

        return GetPlayerByIndex(_localPlayerIndex);

    }

    public Player GetPlayerByIndex(int playerIndex)
    {

        Player value = null;

        if (playerIndex >= 0 && playerIndex < _playersList.Count)
            _playersList[playerIndex].DoIfNotNull(() =>
                value = _playersList[playerIndex]);

        return value;

    }

}
