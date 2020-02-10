using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ServerSkillData
{
    public string username;
    public int id;

    public ServerSkillData() { }
    public ServerSkillData(string username,int id)
    {
        this.username = username;
        this.id = id;
    }
}


public class SkillData
{
    public int id;
    public string name;
    public string des;
    public int atk;
    public int cd;
    public string icon;
    public int price;
    public int mana;
    
    public SkillData() { }
    public SkillData(int id,string name,string des,int atk,int cd,string icon,int price,int mana)
    {
        this.id = id;
        this.name = name;
        this.des = des;
        this.atk = atk;
        this.cd = cd;
        this.icon = icon;
        this.price = price;
        this.mana = mana;
    }

}

public class SkillDataModel : BaseManager<SkillDataModel> {


    string userName = "";

    //存当前玩家拥有的技能表
    private List<SkillData> skillDataList = new List<SkillData>();

    //总表信息，用来存取所有服务器的数据
    private List<ServerSkillData> playerDatas = new List<ServerSkillData>();

    /// <summary>
    /// 返回当前玩家拥有的技能列表
    /// </summary>
    /// <returns></returns>
    public List<SkillData> GetSkillList()
    {
        //找当当前的用户名
        if (userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }
        if (skillDataList.Count == 0)
        {
            //如果没有初始数据，就创建一个用户初始数据
            if (!File.Exists(Application.persistentDataPath + "/SkillData.Json"))
            {
                //创建初始数据，合并服务器数据和本地数据
                //创建服务器数据并存下来
                ServerSkillData serverSkillData = new ServerSkillData(userName,2011);
                playerDatas.Add(serverSkillData);

                //根据服务器数据到本地获取数据
                LocalSkillData localSkillData = LocalSkillDataMgr.Instance.GetSkillDataById(serverSkillData.id);
                //合并数据
                SkillData skillData = new SkillData(serverSkillData.id, localSkillData.name, localSkillData.des, localSkillData.atk,
                    localSkillData.cd, localSkillData.icon, localSkillData.price, localSkillData.mana);
                //存到玩家当前数据
                skillDataList.Add(skillData);

                ServerSkillData serverSkillData2 = new ServerSkillData(userName, 2009);
                playerDatas.Add(serverSkillData2);
                LocalSkillData localSkillData2 = LocalSkillDataMgr.Instance.GetSkillDataById(serverSkillData2.id);
                SkillData skillData2 = new SkillData(serverSkillData2.id, localSkillData2.name, localSkillData2.des, localSkillData2.atk,
                    localSkillData2.cd, localSkillData2.icon, localSkillData2.price, localSkillData2.mana);
                skillDataList.Add(skillData2);

                //读入json
                SaveData();
            }
            else
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/SkillData.Json");
                //读Json
                playerDatas = JsonMapper.ToObject<List<ServerSkillData>>(json);
                //取出表里面当前玩家的数据，存在当前玩家的服务器数据中
                for (int i = 0; i < playerDatas.Count; i++)
                {
                    if(playerDatas[i].username==userName)
                    {
                        //serverSkillData.Add(playerDatas[i]);
                        LocalSkillData localSkillData = LocalSkillDataMgr.Instance.GetSkillDataById(playerDatas[i].id);
                        SkillData skillData2 = new SkillData(localSkillData.id, localSkillData.name, localSkillData.des, localSkillData.atk,
                    localSkillData.cd, localSkillData.icon, localSkillData.price, localSkillData.mana);
                        //服务器数据加上本地数据
                        skillDataList.Add(skillData2);
                    }
                }

            }
            return skillDataList;
        }
        else
        {
            return skillDataList;
        }
    }


    public void SaveData()
    {
        string json = JsonMapper.ToJson(playerDatas);
        File.WriteAllText(Application.persistentDataPath + "/SkillData.Json", json, System.Text.Encoding.UTF8);
    }

    public void AddSkill(SkillData newSkill)
    {
        //给本地加
        skillDataList.Add(newSkill);
        //给服务器加
        ServerSkillData serverSkillData = new ServerSkillData(userName, newSkill.id);
        playerDatas.Add(serverSkillData);
    }

    
}
