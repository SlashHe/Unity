using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : CtrlBasePanel {
    LoginView loginView;

    protected override void Awake()
    {
        base.Awake();
        loginView = GetView<LoginView>("Input");
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            

            case "CloseButton":
                UIManager.Instance.HidePanel("UserLoginPanel");
                break;

            case "LoginButton":
                string userName = loginView.GetUserName();
                string password = loginView.GetPassword();

                if (UserModel.Instance.GetPasswordByName(userName)==null)
                {
                    Debug.Log("用户名错误");
                    //TipsPanel.Instance.Init("用户名错误");
                    loginView.SetUserName("");
                    loginView.SetPassword("");
                    return;
                }

                if (UserModel.Instance.GetPasswordByName(userName) != password)
                {

                    Debug.Log("密码错误");
                    loginView.SetPassword("");
                    //TipsPanel.Instance.Init("密码错误");
                    return;
                }
                UserData myUserData = new UserData(userName, password);

                UIManager.Instance.ShowPanel<ServerPlaneView>("Start/ServerPanel",(obj)=> {
                UIManager.Instance.HidePanel("StartPlane");
                UIManager.Instance.HidePanel("UserLoginPanel");
                    //保存当前用户
                    UserModel.Instance.SaveMyUser(myUserData);
                });
                break;
            default:
                break;
        }
    }

}
