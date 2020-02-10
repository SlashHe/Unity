using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ServerPlaneController : CtrlBasePanel
{

    //从Model得到数据
    private List<ServerData> serverDatasList;
    SelectCallback selectServerCallback;
    //需要管理的面板
    ServerPlaneView serverPlaneView;

    protected override void Awake()
    {
        base.Awake();       
        //初始化服务器数据
        serverDatasList = ServerDataModel.Instance.GetRecData();
        serverPlaneView = GetView<ServerPlaneView>("ServerList");

    }

    private void Start()
    {
        

        //显示服务器列表的数据
    }

    //显示View面板
    public void Init(SelectCallback action)
    {
        selectServerCallback = action;
        //初始化View去显示面板
        serverPlaneView.Init(action, serverDatasList);

    }

    //toggle组事件
    protected override void onValueChanged(string btnName, bool b)
    {
        switch (btnName)
        {
            case "MyToggle":
                if (b)
                {
                    Debug.Log("我的");
                    //得到数据
                    if (UserModel.Instance.GetMyUser().serverName == null)
                    {
                        Debug.Log("没有");
                        serverDatasList = new List<ServerData>();
                    }
                    else
                    {
                        Debug.Log("有");
                        serverDatasList = ServerDataModel.Instance.GetUserData();
                    }
                    //跟新面版数据
                    serverPlaneView.Init(selectServerCallback, serverDatasList);
                }
                //serverPlaneView.Init(action, serverDatasList);
                break;
            case "RcToggle":
                
                if (b)
                {
                    Debug.Log("RcToggle" + b);
                    //得到数据
                    serverDatasList = ServerDataModel.Instance.GetRecData();
                    //跟新面版数据
                    serverPlaneView.Init(selectServerCallback, serverDatasList);
                }               
                break;
            case "AllToggle":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    serverDatasList = ServerDataModel.Instance.GetAllData();
                    //跟新面版数据
                    serverPlaneView.Init(selectServerCallback, serverDatasList);
                }               
                break;
            default:

                break;
        }
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Close":
                Debug.Log("");
                UIManager.Instance.HidePanel("ServerSelecPanel");
                break;
            default:
                break;
        }
    }


}
