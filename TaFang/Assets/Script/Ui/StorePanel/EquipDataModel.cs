using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ServerEquipData
{
    public string username;
    public int id;

    public ServerEquipData() { }
    public ServerEquipData(string username, int id)
    {
        this.username = username;
        this.id = id;
    }
}

public class EquipData
{
    public int id;
    public EquipType equipType;
    public string des;
    public int addAtk;
    public int addDef;
    public int addMana;
    public int addHp;
    public int addSpeed;
    public int price;
    public string name;
    public string icon;

    public EquipData() { }
    public EquipData(int id, string name, string des, int addAtk, int addDef, string icon, int price,
        int addMana,int addHp, int addSpeed, EquipType equipType)
    {
        this.id = id;
        this.name = name;
        this.des = des;
        this.addAtk = addAtk;
        this.addDef = addDef;
        this.icon = icon;
        this.price = price;
        this.addMana = addMana;
        this.addHp = addHp;
        this.addSpeed = addSpeed;
        this.equipType = equipType;
    }

}

public class EquipDataModel : BaseManager<EquipDataModel> {

    string userName = "";

    //存当前玩家拥有的装备表
    private List<EquipData> EquipDataList = new List<EquipData>();

    //总表信息，用来存取所有服务器的数据
    private List<ServerEquipData> playerDatas = new List<ServerEquipData>();

    //保存对应类型装备的列表
    private List<EquipData> armorList = null;
    private List<EquipData> helmetList = null;
    private List<EquipData> necklaceList = null;
    private List<EquipData> ringList = null;
    private List<EquipData> shoesList = null;
    private List<EquipData> weaponList = null;

    /// <summary>
    /// 返回当前玩家拥有的装备列表
    /// </summary>
    /// <returns></returns>
    public List<EquipData> GetEquipList()
    {
        //找当当前的用户名
        if (userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }
        if (EquipDataList.Count == 0)
        {
            //如果没有初始数据，就创建一个用户初始数据
            if (!File.Exists(Application.persistentDataPath + "/EquipData.Json"))
            {
                //创建初始数据，合并服务器数据和本地数据
                //创建服务器数据并存下来
                ServerEquipData serverEquipData = new ServerEquipData(userName, 8001);
                playerDatas.Add(serverEquipData);

                //根据服务器数据到本地获取数据
                LocalEquipData localEquipData = EquipDataMgr.Instance.GetEquipDataById(serverEquipData.id);
                //合并数据
                EquipData equipData = new EquipData(serverEquipData.id, localEquipData.name, localEquipData.des,
                    localEquipData.addAtk, localEquipData.addDef, localEquipData.icon, localEquipData.price,
                    localEquipData.addMana, localEquipData.addHp, localEquipData.addSpeed, localEquipData.equipType);
                //存到玩家当前数据
                EquipDataList.Add(equipData);

                ServerEquipData serverEquipData2 = new ServerEquipData(userName, 8007);
                playerDatas.Add(serverEquipData2);
                LocalEquipData localEquipData2 = EquipDataMgr.Instance.GetEquipDataById(serverEquipData2.id);
                EquipData equipData2 = new EquipData(serverEquipData2.id, localEquipData2.name, localEquipData2.des,
                     localEquipData2.addAtk, localEquipData2.addDef, localEquipData2.icon, localEquipData2.price,
                     localEquipData2.addMana, localEquipData2.addHp, localEquipData2.addSpeed, localEquipData2.equipType);
                EquipDataList.Add(equipData2);

                //读入json
                SaveData();
            }
            else
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/EquipData.Json");
                //读Json
                playerDatas = JsonMapper.ToObject<List<ServerEquipData>>(json);
                //取出表里面当前玩家的数据，存在当前玩家的服务器数据中
                for (int i = 0; i < playerDatas.Count; i++)
                {
                    if (playerDatas[i].username == userName)
                    {
                        //serverSkillData.Add(playerDatas[i]);
                        LocalEquipData localEquipData2 = EquipDataMgr.Instance.GetEquipDataById(playerDatas[i].id);
                        EquipData equipData2 = new EquipData(localEquipData2.id, localEquipData2.name, localEquipData2.des,
                      localEquipData2.addAtk, localEquipData2.addDef, localEquipData2.icon, localEquipData2.price,
                      localEquipData2.addMana, localEquipData2.addHp, localEquipData2.addSpeed, localEquipData2.equipType);
                        //服务器数据加上本地数据
                        EquipDataList.Add(equipData2);
                    }
                }

            }
            return EquipDataList;
        }
        else
        {
            return EquipDataList;
        } 

    }

    /// <summary>
    /// 保存服务器数据
    /// </summary>
    public void SaveData()
    {
        if (playerDatas.Count == 0)
        {
            GetEquipList();
        }
        for (int i = playerDatas.Count-1; i >= 0; i--)
        {
            if(playerDatas[i].username == userName)
            {
                playerDatas.Remove(playerDatas[i]);
            }
        }
        for (int i = 0; i < EquipDataList.Count; i++)
        {
            ServerEquipData serverEquipData = new ServerEquipData(userName, EquipDataList[i].id);
            playerDatas.Add(serverEquipData);
        }
        string json = JsonMapper.ToJson(playerDatas);
        File.WriteAllText(Application.persistentDataPath + "/EquipData.Json", json, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// 增加装备
    /// </summary>
    /// <param name="newEquip"></param>
    public void AddEquip(EquipData newEquip)
    {
        if (armorList == null)
        {
            InitEquipTypeList();
        }
        switch (newEquip.equipType)
        {
            case EquipType.Armor:
                armorList.Add(newEquip);                
                break;
            case EquipType.Helmet:
                helmetList.Add(newEquip);
                break;
            case EquipType.Necklace:
                necklaceList.Add(newEquip);
                break;
            case EquipType.Ring:
                ringList.Add(newEquip);
                break;
            case EquipType.shoes:
                shoesList.Add(newEquip);
                break;
            case EquipType.weapon:
                weaponList.Add(newEquip);
                break;
        }

        //给本地加
        EquipDataList.Add(newEquip);

        
        
    }


    /// <summary>
    /// 移除装备
    /// </summary>
    /// <param name="newEquip"></param>
    public void ReMoveEquip(EquipData newEquip)
    {
        if (armorList == null)
        {
            InitEquipTypeList();
        }
        switch (newEquip.equipType)
        {
            case EquipType.Armor:
                armorList.Remove(newEquip);
                break;
            case EquipType.Helmet:
                helmetList.Remove(newEquip);
                break;
            case EquipType.Necklace:
                necklaceList.Remove(newEquip);
                break;
            case EquipType.Ring:
                ringList.Remove(newEquip);
                break;
            case EquipType.shoes:
                shoesList.Remove(newEquip);
                break;
            case EquipType.weapon:
                weaponList.Remove(newEquip);
                break;
        }
        //移除本地数据
        EquipDataList.Remove(newEquip);

    }

    
    /// <summary>
    /// 本地装备转换为游戏装备
    /// </summary>
    /// <param name="myLocalEquipData"></param>
    /// <returns></returns>
    public EquipData Local2MyEquipData(LocalEquipData myLocalEquipData)
    {
        EquipData equipData = new EquipData(myLocalEquipData.id, myLocalEquipData.name, myLocalEquipData.des,
                   myLocalEquipData.addAtk, myLocalEquipData.addDef, myLocalEquipData.icon, myLocalEquipData.price,
                   myLocalEquipData.addMana, myLocalEquipData.addHp, myLocalEquipData.addSpeed, myLocalEquipData.equipType);
        return equipData;
    }

    /// <summary>
    /// 游戏装备转换为本地装备
    /// </summary>
    /// <param name="myLocalEquipData"></param>
    /// <returns></returns>
    public LocalEquipData MyEquipData2Local(EquipData myEquipData)
    {
        LocalEquipData equipData = new LocalEquipData(myEquipData.id, myEquipData.name, myEquipData.des,
                   myEquipData.addAtk, myEquipData.addDef, myEquipData.icon, myEquipData.price,
                   myEquipData.addMana, myEquipData.addHp, myEquipData.addSpeed, myEquipData.equipType);
        return equipData;
    }

    /// <summary>
    /// 通过装备类型返回装备列表
    /// </summary>
    /// <param name="equipType"></param>
    /// <returns></returns>
    public List<EquipData> GetEquipListByType(EquipType equipType)
    {
        if (EquipDataList.Count == 0)
        {
            GetEquipList();
        }
        if (armorList==null)
        {
            InitEquipTypeList();
        }

        switch (equipType)
        {
            case EquipType.Armor:
                return armorList;
                break;
            case EquipType.Helmet:
                return helmetList;
                break;
            case EquipType.Necklace:
                return necklaceList;
                break;
            case EquipType.Ring:
                return ringList;
                break;
            case EquipType.shoes:
                return shoesList;
                break;
            case EquipType.weapon:
                return weaponList;
                break;
        }
        return null;
    }

    public void InitEquipTypeList()
    {
        if (EquipDataList.Count == 0)
        {
            GetEquipList();
        }
        armorList = new List<EquipData>();
        helmetList = new List<EquipData>();
        necklaceList = new List<EquipData>();
        ringList = new List<EquipData>();
        shoesList = new List<EquipData>();
        weaponList = new List<EquipData>();
        for (int i = 0; i < EquipDataList.Count; i++)
        {
            if ((EquipType)EquipDataList[i].equipType == EquipType.Armor)
            {
                armorList.Add(EquipDataList[i]);
            }
            if ((EquipType)EquipDataList[i].equipType == EquipType.Helmet)
            {
                helmetList.Add(EquipDataList[i]);
            }
            if ((EquipType)EquipDataList[i].equipType == EquipType.Necklace)
            {
                necklaceList.Add(EquipDataList[i]);
            }
            if ((EquipType)EquipDataList[i].equipType == EquipType.Ring)
            {
                ringList.Add(EquipDataList[i]);
            }
            if ((EquipType)EquipDataList[i].equipType == EquipType.shoes)
            {
                shoesList.Add(EquipDataList[i]);
            }
            if ((EquipType)EquipDataList[i].equipType == EquipType.weapon)
            {
                weaponList.Add(EquipDataList[i]);
            }
        }
    }
}
