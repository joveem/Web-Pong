using System.Threading.Tasks;
using UnityEngine;

//using WebSocketSharp;
using NativeWebSocket;

using JovDK.Debug;
using JovDK.SafeActions;

public partial class NetworkManager : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    WebSocket _webSocketClient;
    [SerializeField] GameManager _gameManager;

    [SerializeField] MainMenuPanel _mainMenuPanel;

    // [Space(5), Header("[ State ]"), Space(10)]
    // [Space(5), Header("[ Parts ]"), Space(10)]

    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] string _serverUrl = "ws://localhost:8029";


    void Awake()
    {

        SetupComponent();
        SetupEvents();

    }

    void SetupComponent()
    {

        _gameManager.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _gameManager), false);
        _mainMenuPanel.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _mainMenuPanel), false);

        _mainMenuPanel.DoIfNotNull(() =>
        {

            _mainMenuPanel.PlayButtonAction += PlayButton;
            _mainMenuPanel.CancelButtonAction += CancelButton;

        });

    }

    async void Start()
    {

        await SetupSocketClient();
        Connect();

    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocketClient.DispatchMessageQueue();
#endif
    }


    void OnDestroy()
    {

        DebugExtension.DevLog("webSocketClient.State = " + _webSocketClient.State);

        if (_webSocketClient.State != WebSocketState.Closed)
            _webSocketClient.Close();

    }

    async Task SetupSocketClient()
    {

        _webSocketClient = new WebSocket(_serverUrl);

        _webSocketClient.OnOpen += OnConnected;
        _webSocketClient.OnError += OnError;
        _webSocketClient.OnClose += OnClose;
        _webSocketClient.OnMessage += OnMessage;
        // webSocketClient.OnMessage += (bytes) =>
        // {
        //     // Reading a plain text message
        //     var message = System.Text.Encoding.UTF8.GetString(bytes);
        //     Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
        // };
        // webSocketClient.OnMessage += (aaaa) =>
        // {

        //     DebugExtension.DevLog("MESSAGE!");

        // };

    }

}
