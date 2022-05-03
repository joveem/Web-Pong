using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.SafeActions;
using JovDK.Debug;

public class DefaultPanel : MonoBehaviour
{

    [Space(5), Header("[ Panel State ]"), Space(10)]

    public bool IsShowingPanel = false;
    [SerializeField] Transform _panelContainer;


    public void ShowPanel()
    {

        IsShowingPanel = true;
        _panelContainer.SetActiveIfNotNull(IsShowingPanel);

    }

    public void HidePanel()
    {

        IsShowingPanel = false;
        _panelContainer.SetActiveIfNotNull(IsShowingPanel);

    }

    public void SwitchPanelView()
    {

        if (IsShowingPanel)
            HidePanel();
        else
            ShowPanel();

    }

}
