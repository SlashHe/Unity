using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelPanelController : CtrlBasePanel {

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Close":
                Debug.Log("");
                UIManager.Instance.HidePanel("GameLevelPanel");
                break;
            default:
                break;
        }
    }
}
