using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.SafeActions;
using JovDK.Debug;

public class MainMenuPanel : DefaultPanel
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    [Space(5), Header("[ State ]"), Space(10)]

    public Action PlayButtonAction = null;
    public Action CancelButtonAction = null;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Button _playButton;
    [SerializeField] Button _cancelButton;
    [SerializeField] TextMeshProUGUI _statusText;


    // [Space(5), Header("[ Configs ]"), Space(10)]


    void Start()
    {

        ResetPanel();
        SetupButtons();

    }

    public void SetupButtons()
    {

        _playButton.SetOnClickIfNotNull(() => PlayButtonAction.DoIfNotNull(PlayButtonAction));
        _cancelButton.SetOnClickIfNotNull(() => CancelButtonAction.DoIfNotNull(CancelButtonAction));

    }

    public void ResetPanel(bool interactablePlayButton = false)
    {

        _playButton.DoIfNotNull(() =>
        {

            _playButton.gameObject.SetActive(true);
            _playButton.interactable = interactablePlayButton;

        });

        _cancelButton.DoIfNotNull(() =>
        {

            _cancelButton.gameObject.SetActive(false);

        });

        SetStatusText("");

    }

    public void SetStatusText(string text)
    {

        _statusText.DoIfNotNull(() => _statusText.text = text);

    }

    public void ShowPlayButton()
    {

        _playButton.SetActiveIfNotNull(true);
        _cancelButton.SetActiveIfNotNull(false);

    }

    public void ShowCancelButton()
    {

        _cancelButton.SetActiveIfNotNull(true);
        _playButton.SetActiveIfNotNull(false);

    }

    public void EnableButtons()
    {

        _playButton.SetInteractableIfNotNull(true);
        _cancelButton.SetInteractableIfNotNull(true);

    }

    public void DisableButtons()
    {

        _playButton.SetInteractableIfNotNull(false);
        _cancelButton.SetInteractableIfNotNull(false);

    }

}
