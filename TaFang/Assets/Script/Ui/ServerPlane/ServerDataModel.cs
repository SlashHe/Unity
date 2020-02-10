using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

/// <summary>
/// Model类用来存取数据用的
/// </summary>
public class ServerDataModel : BaseManager<ServerDataModel> {

    //list来存数据
    public List<ServerData> serverDatasList;
    public List<ServerData> serverRecDatasList;
    public List<ServerData> serverUserDatasList;
    Dictionary<string, ServerData> serverDic = new Dictionary<string, ServerData>();
    /// <summary>
    /// 返回所有服务器数据
    /// </summary>
    /// <returns></returns>
    public List<ServerData> GetAllData()
    {
        //从Json中读数据
        if (serverDatasList != null)
        {
            return serverDatasList;
        }
        else
        {
            TextAsset json = ResManager.Instance.Load<TextAsset>("Config/ServerData");
            List<ServerData> serverlist = JsonMapper.ToObject<List<ServerData>>(json.text);
            serverDatasList = serverlist;
            for (int i = 0; i < serverlist.Count; i++)
            {
            
                serverDic.Add(serverlist[i].serverName, serverlist[i]);
            }
        }
        return serverDatasList;
    }


    /// <summary>
    /// 返回推荐服务器的数据
    /// </summary>
    /// <returns></returns>
    public List<ServerData> GetRecData()
    {
        if(serverRecDatasList != null)
        {
            return serverRecDatasList;
        }
        serverRecDatasList = new List<ServerData>();
        if (serverDatasList == null)
        {
            GetAllData();
        }
        for (int i = 0; i < serverDatasList.Count; i++)
        {            
            if (serverDatasList[i].serverType == ServerType.Fluen)
            {
                serverRecDatasList.Add(serverDatasList[i]);
            }
        }
        return serverRecDatasList;

    }

    /// <summary>
    /// 返回玩家的服务器数据
    /// </summary>
    public List<ServerData> GetUserData()
    {
        if (serverUserDatasList != null)
        {
            return serverUserDatasList;
        }
        serverUserDatasList = new List<ServerData>();
        UserData user = UserModel.Instance.GetMyUser();

        serverUserDatasList.Add(serverDic[user.serverName]);
        return serverUserDatasList;
    }
    //存数据
    public void SaveData()
    {
        string json = JsonMapper.ToJson(serverDatasList);

        //写入数据
        File.WriteAllText(Application.persistentDataPath + "/ServerData.Json", json, System.Text.Encoding.UTF8);
    }
}
