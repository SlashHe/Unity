using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelController : CtrlBasePanel {


    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "LoginButton":
                UIManager.Instance.ShowPanel("Start/UserLoginPanel");
                break;
            case "RegisterButton":
                UIManager.Instance.ShowPanel("Start/UserRegisterPanel");
                break;
            default:
                break;
        }
    }

}
