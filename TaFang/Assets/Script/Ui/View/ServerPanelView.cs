using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerPanelView : BasePanel {

    

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "ServeSelec":
                UIManager.Instance.ShowPanel<ServerPlaneController>("Start/ServerSelecPanel", (panel) => {
                    //更新面板上的数据
                   // panel.Init(UpdateData);
                });               
                break;
            case "LoginButton":
                //进入游戏
                
                //UIManager.Instance.ShowPanel<StartPlaneView>("GameMain/MainPanel");
                UIManager.Instance.ShowPanel<LoginUi>("Logining/LoginingPanel", (obj) => {
                    //更新面板上的数据
                    SceneMgr.Instance.LoadScene("Scene2");
                    UIManager.Instance.HidePanel("Start/ServerPanel");
                });
                
                
                break;
            default:
                break;
        }
    }


    public void UpdateData(ServerData data)
    {
        string path;
        GetControl<Text>("ServeNameText").text = data.serverName;
        switch (data.serverType)
        {
            case ServerType.Fluen:
                path = "UI/xuanqv/lv";
                break;
            case ServerType.Crowd:
                path = "UI/xuanqv/hong";
                break;
            case ServerType.Maintain:
                path = "UI/xuanqv/weihu";
                break;
            default:
                path = null;
                break;
        }
        GetControl<Image>("ServeTypeImage").sprite = Resources.Load<Sprite>(path);
    }


    
}
