using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;
using JovDK.Debug;

public partial class GameManager : MonoBehaviour
{



    public void SetupMatch(int localPlayerIndex, int controllerPlayerIndex)
    {

        _localPlayerIndex = localPlayerIndex;
        IsMatchStarted = true;

        ResetScenario();
        ResetPanels(_localPlayerIndex);

        _inputManager.IsControlEnable = true;
        SetCamaraView(_localPlayerIndex);

        for (int i = 0; i < _playersList.Count; i++)
        {

            _playersList[i].Index = i;

        }

        _ball.DoIfNotNull(() =>
        {

            _ball.SetLocalPlayerIndex(localPlayerIndex);
            _ball.SetControllerPlayerIndex(1);

            _ball.ResetPosition(controllerPlayerIndex);
            _ball.DisableBall();

        });

    }

    public void StartRound(int controllerPlayerIndex)
    {

        _ball.DoIfNotNull(() =>
        {

            _ball.ResetPosition(controllerPlayerIndex);
            _ball.ShowBallWithDrop();
            _ball.RestartComponentInNewRound();

        });

    }

    void SetCamaraView(int playerIndex)
    {

        mainCameraPivot.DoIfNotNull(() =>
        {

            if (playerIndex == 0)
                mainCameraPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                mainCameraPivot.transform.rotation = Quaternion.Euler(0, 180, 0);

        });

    }

    void ResetPanels(int localPlayerIndex)
    {

        _matchScorePanel.DoIfNotNull(() =>
            _matchScorePanel.ShowPanel());

        ResetScore(localPlayerIndex);

    }

    void ResetScore(int localPlayerIndex)
    {

        _matchScorePanel.DoIfNotNull(() =>
        {

            _matchScorePanel.ResetPanel();
            _matchScorePanel.SetLocalPlayerPosition(localPlayerIndex);

        });

        _matchMenuPanel.DoIfNotNull(() =>
            _matchMenuPanel.SetLocalPlayerPosition(localPlayerIndex));

    }

    void ResetScenario()
    {

        foreach (Player player in _playersList)
        {

            player.DoIfNotNull(() =>
                player.DisablePlayer());

        }

        _ball.DoIfNotNull(() =>
            _ball.ResetPosition());

    }

    public void FinishMatch()
    {

        IsMatchStarted = false;
        _inputManager.IsControlEnable = false;

        // show ending alert

    }

    public void ExitGame()
    {

        ResetScenario();

        _inputManager.IsControlEnable = false;

        _matchScorePanel.HidePanel();
        _matchScorePanel.ResetPanel();
        _matchMenuPanel.HidePanel();
        _mainMenuPanel.ShowPanel();
        _mainMenuPanel.ResetPanel(true);

    }

}
