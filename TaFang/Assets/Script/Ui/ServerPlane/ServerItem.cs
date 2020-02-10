using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ServerType
{
    Fluen=0,
    Crowd =1,
    Maintain =2,
}

public class ServerData {

    public ServerType serverType;
    public string serverName;

    public ServerData() { }
    public ServerData(string serverName,ServerType serverType)
    {
        this.serverType = serverType;
        this.serverName = serverName;
    }
}

public delegate void SelectCallback(ServerData data);
public class ServerItem : MonoBehaviour
{

    ServerData serverData;
    public Button itemBtn;
    public Image stateImage;
    public Text serverName;

    private static Sprite hotState, normalState, upholdSate;

    SelectCallback selectServerCallback;
    /// <summary>
    /// 克隆并且初始化服务器Item
    /// </summary>
    /// <param name="data"></param>
    /// <param name="callback"></param>
    public void Init(ServerData data, SelectCallback callback)
    {
        serverData = data;
        selectServerCallback = callback;
        itemBtn.onClick.AddListener(Click);

        //初始化名字
        serverName.text = data.serverName;
        //初始化图标
        if (hotState == null)
        {
            hotState = Resources.Load<Sprite>("UI/xuanqv/hong");
            normalState = Resources.Load<Sprite>("UI/xuanqv/lv");
            upholdSate = Resources.Load<Sprite>("UI/xuanqv/weihu");
        }

        
        switch (data.serverType)
        {
            case ServerType.Crowd:
                stateImage.sprite = hotState;
                break;
            case ServerType.Fluen:
                stateImage.sprite = normalState;
                break;
            case ServerType.Maintain:
                stateImage.sprite = upholdSate;
                break;
            default:
                break;//
        }
    }

    public void Click()
    {
        
        //执行委托函数，想要在点击按钮后做什么，传进去的是serverItem的数据
        selectServerCallback(serverData);
        //隐藏面板
        UIManager.Instance.HidePanel("RightDes");
        UIManager.Instance.HidePanel("ServerSelecPanel");
    }

}
