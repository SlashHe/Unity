using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public string userName;
    public int lv;
    private int exp;
    public int Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp += value;
            if (exp >= 100)
            {
                exp -= 100;
                lv += 1;
            }
        }
    }
    public string name;
    public int coin;
    public int diamond;
    public int power;

    public PlayerData() { }

    public PlayerData(string userName,int lv, int exp, int coin, int diamond, int power)
    {
        name = "Jocker";
        this.userName = userName;
        this.lv = lv;
        this.exp = exp;
        this.coin = coin;
        this.diamond = diamond;
        this.power = power;
    }
}

/// <summary>
/// 管理着玩家金币等玩家数据
/// </summary>
public class MainPanelModel : BaseManager<MainPanelModel> {

    //当前游戏的玩家数据
    private PlayerData myPlayerData;
    //所有玩家的游戏数据,用来存表用的
    private Dictionary<string, PlayerData> playerDataDic = new Dictionary<string, PlayerData>();
    string userName="";

    //从服务器获取数据

    public PlayerData GetPlayerData()
    {
        //找当当前的用户名
        if(userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }
        //如果没有就去找服务器要
        if (myPlayerData == null)
        {
            //如果没有初始数据，就创建一个用户初始数据
            if (!File.Exists(Application.persistentDataPath + "/PlayerData.Json"))
            {
                myPlayerData = new PlayerData(userName, 1,0,4000,3000,70);
                playerDataDic.Add(myPlayerData.userName, myPlayerData);
                //读入json
                SaveData();
            }
            else
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.Json");
                //读Json
                List<PlayerData> playerDatas = JsonMapper.ToObject<List<PlayerData>>(json);
                //把所有信息存到字典中
                for (int i = 0; i < playerDatas.Count; i++)
                {
                    playerDataDic.Add(playerDatas[i].userName, playerDatas[i]);
                }
                if (playerDataDic.ContainsKey(userName))
                {
                    myPlayerData = playerDataDic[userName];
                }
                else
                {
                    myPlayerData = new PlayerData(userName, 1, 0, 4000, 3000, 70);
                    playerDataDic.Add(myPlayerData.userName, myPlayerData);
                    SaveData();
                }
            }
            return myPlayerData;
        }
        else
        {
            return myPlayerData;
        }
    }


    /// <summary>
    /// 保存JSon到本地服务器库
    /// </summary>
    public void SaveData()
    {
        List<PlayerData> playerDatas = new List<PlayerData>();
        //字典转换为List
        foreach (var item in playerDataDic)
        {
            playerDatas.Add(item.Value);
        }
        string json = JsonMapper.ToJson(playerDatas);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.Json", json, System.Text.Encoding.UTF8);
    }
}
