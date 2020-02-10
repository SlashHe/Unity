using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    /// <summary>
    /// 盔甲
    /// </summary>
    Armor =0,
    /// <summary>
    /// 头盔
    /// </summary>
    Helmet =1,
    /// <summary>
    /// 项链
    /// </summary>
    Necklace =2,
    /// <summary>
    /// 戒子
    /// </summary>
    Ring =3,
    /// <summary>
    /// 鞋子
    /// </summary>
    shoes =4,
    /// <summary>
    /// 武器
    /// </summary>
    weapon =5,
}

public class LocalEquipData
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

    public LocalEquipData() { }
    public LocalEquipData(int id, string name, string des, int addAtk, int addDef, string icon, int price,
        int addMana, int addHp, int addSpeed, EquipType equipType)
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

public class EquipDataMgr : BaseManager<EquipDataMgr> {

    private Dictionary<int, LocalEquipData> localEquipDataDic = new Dictionary<int, LocalEquipData>();

    private List<LocalEquipData> localEquipDataList = new List<LocalEquipData>();

    private List<LocalEquipData> armorList = new List<LocalEquipData>();
    private List<LocalEquipData> helmetList = new List<LocalEquipData>();
    private List<LocalEquipData> necklaceList = new List<LocalEquipData>();
    private List<LocalEquipData> ringList = new List<LocalEquipData>();
    private List<LocalEquipData> shoesList = new List<LocalEquipData>();
    private List<LocalEquipData> weaponList = new List<LocalEquipData>();

    private void Init()
    {
        //从本地读取数据
        TextAsset json = Resources.Load<TextAsset>("Config/EquipData");
        localEquipDataList = JsonMapper.ToObject<List<LocalEquipData>>(json.text);
        for (int i = 0; i < localEquipDataList.Count; i++)
        {
            localEquipDataDic.Add(localEquipDataList[i].id, localEquipDataList[i]);
        }

    }

    public LocalEquipData GetEquipDataById(int id)
    {
        //获取数据的时候初始化
        if (localEquipDataDic.Count == 0)
        {
            Init();
        }
        if (localEquipDataDic.ContainsKey(id))
        {
            return localEquipDataDic[id];
        }
        else
        {
            return null;
        }
    }

    public List<LocalEquipData> GetEquipDataByType(EquipType equipType)
    {
        if (localEquipDataDic.Count == 0)
        {
            Init();
        }
        if (armorList.Count == 0)
        {
            for (int i = 0; i < localEquipDataList.Count; i++)
            {
                if ((EquipType)localEquipDataList[i].equipType == EquipType.Armor)
                {
                    armorList.Add(localEquipDataList[i]);
                }
                if ((EquipType)localEquipDataList[i].equipType == EquipType.Helmet)
                {
                    helmetList.Add(localEquipDataList[i]);
                }
                if ((EquipType)localEquipDataList[i].equipType == EquipType.Necklace)
                {
                    necklaceList.Add(localEquipDataList[i]);
                }
                if ((EquipType)localEquipDataList[i].equipType == EquipType.Ring)
                {
                    ringList.Add(localEquipDataList[i]);
                }
                if ((EquipType)localEquipDataList[i].equipType == EquipType.shoes)
                {
                    shoesList.Add(localEquipDataList[i]);
                }
                if ((EquipType)localEquipDataList[i].equipType == EquipType.weapon)
                {
                    weaponList.Add(localEquipDataList[i]);
                }
            }
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
}