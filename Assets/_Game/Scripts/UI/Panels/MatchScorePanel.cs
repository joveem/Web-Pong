using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.SafeActions;
using JovDK.Debug;
using JovDK.SerializingTools.Json;

public class MatchScorePanel : DefaultPanel
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]
    [Space(5), Header("[ State ]"), Space(10)]

    PlayerIndexColor _localPlayerPositionColor = PlayerIndexColor.Red;
    PlayerIndexColor _networkPlayerPositionColor = PlayerIndexColor.Blue;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] TextMeshProUGUI _localPlayerScoreText;
    [SerializeField] TextMeshProUGUI _networkPlayerScoreText;
    [SerializeField] TextMeshProUGUI _latencyCounterText;


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] const string _redColorHexCode = "#f54";
    [SerializeField] const string _blueColorHexCode = "#26f";


    void Start()
    {

        ResetPanel();
        HidePanel();

    }


    public void ResetPanel()
    {

        SetPlayersScores(new int[] { 0, 0 });
        SetLatency(0);

    }

    string ColorizeText(string text, string colorHex)
    {

        return "<color=" + colorHex + ">" + text + "</color>";

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

    public void SetLatency(int latencyMilliseconds)
    {

        string latencTextPrefix = "";
        string latencTextSufix = "";

        if (latencyMilliseconds > 999)
        {

            latencTextPrefix = "<color=#f11>+";
            latencyMilliseconds = 999;
            latencTextSufix = "</color=>+";

        }

        string latencText = latencTextPrefix + latencyMilliseconds + "ms" + latencTextSufix;

        _latencyCounterText.DoIfNotNull(() =>
            _latencyCounterText.text = latencText);

    }
    public void SetLatency(float latencySeconds)
    {

        int latencyMilliseconds = Mathf.RoundToInt(latencySeconds * 1000);
        SetLatency(latencyMilliseconds);

    }

    enum PlayerIndexColor { Red = 0, Blue = 1 }

}
