using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerView : ViewBasePanel {

    
    public void updateServerName(ServerData data)
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

    public string GetServeName()
    {
        return GetControl<Text>("ServeNameText").text;
    }
}
