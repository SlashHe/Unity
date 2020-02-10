using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : CtrlBasePanel {

    RegisterView registerView;
    

    protected override void Awake()
    {
        base.Awake();
        
    }
    private void Start()
    {
        registerView = GetView<RegisterView>("Input");
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "RegisterButton":

                UserData user;
                user = registerView.GetUser();
                if (user == null)
                {

                    //两次密码不对
                    //弹出提示框
                    //重新输入
                    return;
                }
                else
                {

                    //读入账号密码，并保存
                    UserModel.Instance.AddUserData(user);
                    UserModel.Instance.SaveData();
                    //关闭界面
                    UIManager.Instance.HidePanel("UserRegisterPanel");
                    UIManager.Instance.ShowPanel("Start/StartPlane");
                }

                break;
            case "CloseButton":
                UIManager.Instance.HidePanel("UserRegisterPanel");

                break;

            
            default:
                break;
        }
    }
}
