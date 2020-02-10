using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerController : CtrlBasePanel {

    

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "ServeSelec":
                
                UIManager.Instance.ShowPanel<ServerPlaneController>("Start/ServerSelecPanel", (panel) => {
                    //更新面板上的数据
                    panel.Init(UpdateData);
                });
                break;
            case "LoginButton":
                //进入游戏
                //更新用户数据
                UserData user = UserModel.Instance.GetMyUser();
                user.serverName = GetView<ServerView>("ServeSelec").GetServeName();
                UserModel.Instance.SaveMyUser(user);
                UserModel.Instance.AddUserData(user);
                UserModel.Instance.SaveData();
                UIManager.Instance.ShowPanel<LoginUi>("Logining/LoginingPanel", (obj) => {
                    //更新面板上的数据
                    SceneMgr.Instance.LoadScene("Scene2");
                    UIManager.Instance.HidePanel("ServerPanel");
                });

                break;
            default:
                break;
        }
    }

    public void UpdateData(ServerData data)
    {
        GetView<ServerView>("ServeSelec").updateServerName(data);
        
    }


}
