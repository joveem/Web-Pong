using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;
using JovDK.Debug;

public partial class GameManager : MonoBehaviour
{

    void Awake()
    {

        SetupComponent();

    }

    void SetupComponent()
    {

        _mainMenuPanel.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _mainMenuPanel), false);
        _networkManager.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _networkManager), false);
        _matchScorePanel.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _matchScorePanel), false);
        _matchMenuPanel.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _matchMenuPanel), false);
        _inputManager.DoIfNull(() => SafeActionsTools.TryFindGameObject(out _inputManager), false);

        _matchMenuPanel.DoIfNotNull(() => _matchMenuPanel.ExitGameAction = ExitGame);

    }

}
