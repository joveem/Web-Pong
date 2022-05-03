using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.SafeActions;
using JovDK.Debug;
using JovDK.SerializingTools.Json;

public class MatchMenuPanel : DefaultPanel
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]
    [Space(5), Header("[ State ]"), Space(10)]

    PlayerIndexColor _localPlayerPositionColor = PlayerIndexColor.Red;
    PlayerIndexColor _networkPlayerPositionColor = PlayerIndexColor.Blue;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _subTitleText;
    [SerializeField] TextMeshProUGUI _localPlayerScoreText;
    [SerializeField] TextMeshProUGUI _networkPlayerScoreText;
    [SerializeField] Button _exitGameButton;


    [Space(5), Header("[ Configs ]"), Space(10)]

    public Action ExitGameAction = null;

    [SerializeField] const string _redColorHexCode = "#f54";
    [SerializeField] const string _blueColorHexCode = "#26f";

    [SerializeField] const string _winColorHexCode = "#2d2";
    [SerializeField] const string _loseColorHexCode = "#d62";



    void Start()
    {

        ResetPanel();
        HidePanel();
        SetupButtons();

    }

    void SetupButtons()
    {

        _exitGameButton.SetOnClickIfNotNull(ExitGameButton);

    }

    void ExitGameButton()
    {

        ExitGameAction.DoIfNotNull(ExitGameAction);

    }

    string ColorizeText(string text, string colorHex)
    {

        return "<color=" + colorHex + ">" + text + "</color>";

    }

    public void ShowWinAlert(int[] playersScores, bool hasOpponentGaveUp = false)
    {

        ShowPanel();
        GameManager.instance.MatchScorePanel.HidePanel();

        SetTitle("YOU WIN!", _winColorHexCode);
        _subTitleText.SetActiveIfNotNull(hasOpponentGaveUp);

        SetPlayersScores(playersScores);

    }

    public void ShowLoseAlert(int[] playersScores, bool hasOpponentGaveUp = false)
    {

        ShowPanel();
        GameManager.instance.MatchScorePanel.HidePanel();

        SetTitle("YOU LOSE!", _loseColorHexCode);
        _subTitleText.SetActiveIfNotNull(hasOpponentGaveUp);

        SetPlayersScores(playersScores);

    }

    public void SetTitle(string text, string colorHexCode)
    {

        _titleText.DoIfNotNull(() =>
            _titleText.text = ColorizeText(text, colorHexCode));

    }


    public void ResetPanel()
    {

        SetPlayersScores(new int[] { 0, 0 });

    }

    public void SetLocalPlayerPosition(int localPlayerPosition)
    {

        switch (localPlayerPosition)
        {

            case 0:
                {

                    _localPlayerPositionColor = PlayerIndexColor.Red;
                    _networkPlayerPositionColor = PlayerIndexColor.Blue;
                    break;

                }
            case 1:
                {

                    _networkPlayerPositionColor = PlayerIndexColor.Red;
                    _localPlayerPositionColor = PlayerIndexColor.Blue;
                    break;

                }
            default:
                {

                    DebugExtension.DevLogError("Cannot parse <int>localPlayerPosition as <PlayerPositionColor>localPlayerPosition!" +
                        " ( localPlayerPosition = " + localPlayerPosition + " )");
                    break;

                }

        }

    }

    public void SetLocalPlayerScore(int playerScore)
    {

        _localPlayerScoreText.DoIfNotNull(() =>
        {

            string scoreColor = _localPlayerPositionColor == PlayerIndexColor.Red ?
                                _redColorHexCode :
                                _blueColorHexCode;

            _localPlayerScoreText.text = ColorizeText(playerScore.ToString(), scoreColor);

        });

    }

    public void SetNetWorkPlayerScore(int playerScore)
    {

        _networkPlayerScoreText.DoIfNotNull(() =>
        {

            string scoreColor = _networkPlayerPositionColor == PlayerIndexColor.Red ?
                                _redColorHexCode :
                                _blueColorHexCode;

            _networkPlayerScoreText.text = ColorizeText(playerScore.ToString(), scoreColor);

        });

    }

    public void SetPlayersScores(int[] playersScores)
    {

        if (playersScores.Length == 2)
        {

            SetLocalPlayerScore(playersScores[(int)_localPlayerPositionColor]);
            SetNetWorkPlayerScore(playersScores[(int)_networkPlayerPositionColor]);

        }
        else
            DebugExtension.DevLogError("invalid playersScores lenght!" +
                " ( playersScores = " + playersScores.SerializeObjectToJSON() + " )");

    }


}

public enum PlayerIndexColor { Red = 0, Blue = 1 }
