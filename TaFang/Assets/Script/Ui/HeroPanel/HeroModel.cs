using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeroData
{
    public string userName;
    public string name;
    public int lv;
    public int atk;
    public int hp;
    public int mana;
    public string des;
    public int def;
    public int speed;

    public HeroData() { }
    public HeroData(string userName,int lv,int atk,int hp,int mana,int def,int speed)
    {
        this.userName = userName;
        this.lv = lv;
        this.atk = atk;
        this.hp = hp;
        this.mana = mana;
        this.des = "双刀剑客：隐藏在深山中的剑客，拥有自成一派的独门秘术";
        this.def = def;
        this.speed = speed;
        this.name = "双刀剑客";
    }
}


public class HeroModel : BaseManager<HeroModel> {

    //当前游戏的玩家数据
    private HeroData myHeroData;
    //所有玩家的游戏数据,用来存表用的
    private Dictionary<string, HeroData> heroDataDic = new Dictionary<string, HeroData>();
    string userName = "";

    //从服务器获取数据

    public HeroData GetHeroData()
    {
        //找当当前的用户名
        if (userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }
        //如果没有就去找服务器要
        if (myHeroData == null)
        {
            //如果没有初始数据，就创建一个用户初始数据
            if (!File.Exists(Application.persistentDataPath + "/HeroData.Json"))
            {
                myHeroData = new HeroData(userName,1,40,300,100,10,5);
                heroDataDic.Add(myHeroData.userName, myHeroData);
                //读入json
                SaveData();
            }
            else
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/HeroData.Json");
                //读Json
                List<HeroData> playerDatas = JsonMapper.ToObject<List<HeroData>>(json);
                //把所有信息存到字典中
                for (int i = 0; i < playerDatas.Count; i++)
                {
                    heroDataDic.Add(playerDatas[i].userName, playerDatas[i]);
                }
                if (heroDataDic.ContainsKey(userName))
                {
                    myHeroData = heroDataDic[userName];
                }
                else
                {
                    myHeroData = new HeroData(userName, 1, 40, 300, 100, 10, 5);
                    heroDataDic.Add(myHeroData.userName, myHeroData);
                    SaveData();
                }
            }
            return myHeroData;
        }
        else
        {
            return myHeroData;
        }
    }


    /// <summary>
    /// 保存JSon到本地服务器库
    /// </summary>
    public void SaveData()
    {
        List<HeroData> playerDatas = new List<HeroData>();
        //字典转换为List
        foreach (var item in heroDataDic)
        {
            playerDatas.Add(item.Value);
        }
        string json = JsonMapper.ToJson(playerDatas);
        File.WriteAllText(Application.persistentDataPath + "/HeroData.Json", json, System.Text.Encoding.UTF8);
    }
}
