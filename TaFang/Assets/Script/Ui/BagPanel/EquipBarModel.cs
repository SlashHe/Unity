using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class BarquipData
{
    public string username;
    public int id;

    public BarquipData() { }
    public BarquipData(string username, int id)
    {
        this.username = username;
        this.id = id;
    }
}


public class EquipBarModel : BaseManager<EquipBarModel> {

    //当前玩家的装备栏信息
    List<EquipData> barList = null;

    Dictionary<EquipType, EquipData> barDic = null;

    //所有玩家的装备栏信息
    List<BarquipData> allBarList = null;

    string userName = "";


    //返回当前玩家的装备栏信息
    public List<EquipData> GetBarList()
    {
        if (userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }
        if (barList == null) {
            barList = new List<EquipData>();
            barDic = new Dictionary<EquipType, EquipData>();
            if (!File.Exists(Application.persistentDataPath + "/EquipBarData.Json"))
            {
                allBarList = new List<BarquipData>();
                return barList;
            }
            else {
                string json = File.ReadAllText(Application.persistentDataPath + "/EquipBarData.Json");
                allBarList = JsonMapper.ToObject<List<BarquipData>>(json);
                for (int i = 0; i < allBarList.Count; i++)
                {
                    if (allBarList[i].username == userName) {
                        //数据转换
                        LocalEquipData localEquip = EquipDataMgr.Instance.GetEquipDataById(allBarList[i].id);
                        EquipData equipData = EquipDataModel.Instance.Local2MyEquipData(localEquip);
                        barList.Add(equipData);
                        barDic.Add(equipData.equipType, equipData);
                    }
                }
            }
            return barList;
        }
        return barList;
    }

    //脱掉装备
    public void ReMoveEquip(EquipData newEquip)
    {
        if (allBarList == null)
        {
            GetBarList();
        }
        
        //增加背包数据
        EquipDataModel.Instance.AddEquip(barDic[newEquip.equipType]);
        //移除装备栏数据
        barList.Remove(barDic[newEquip.equipType]);
        barDic.Remove(newEquip.equipType);
        

    }

    //穿戴装备
    public void AddEquip(EquipData newEquip) {
        if (allBarList == null)
        {
            GetBarList();
        }
        //如果已经有了装备，那么现移除，再穿戴
        if (barDic.ContainsKey(newEquip.equipType)) {
            ReMoveEquip(newEquip);
        }
        //在装备栏增加装备
        barList.Add(newEquip);
        barDic.Add(newEquip.equipType, newEquip);
        //在背包里面移除数据
        EquipDataModel.Instance.ReMoveEquip(newEquip);
    }

    //保存服务器数据
    public void SaveData() {
        if (allBarList == null) {
            GetBarList();
        }
        //移除当前名字的服务器数据
        for (int i = allBarList.Count-1; i>= 0; i--)
        {
            if (allBarList[i].username == userName) {
                allBarList.Remove(allBarList[i]);
            }
        }
        //重新添加
        for (int i = 0; i < barList.Count; i++)
        {
            BarquipData barquip = new BarquipData(userName, barList[i].id);
            allBarList.Add(barquip);
        }

        //保存JSon
        string json = JsonMapper.ToJson(allBarList);
        File.WriteAllText(Application.persistentDataPath + "/EquipBarData.Json", json, System.Text.Encoding.UTF8);
    }
}
