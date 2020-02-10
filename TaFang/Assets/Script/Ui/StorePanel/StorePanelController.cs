using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : CtrlBasePanel {

    private List<LocalEquipData> equipDatasList;

    StorePanalView storePanalView;
    LocalEquipData myLocalEquipData = null;

    protected override void Awake()
    {
        base.Awake();
        equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.weapon);
        storePanalView = GetView<StorePanalView>("CommodityList");

    }
    // Use this for initialization
    void Start () {
        Init();
    }

    public void Init()
    {

        storePanalView.Init(equipDatasList, OnClickBuy);

    }

    //toggle组事件
    protected override void onValueChanged(string btnName, bool b)
    {
        switch (btnName)
        {
            case "Weapon":
                if (b)
                {
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.weapon);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
                }

                break;
            case "Armor":

                if (b)
                {
                    
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.Armor);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
                }
                break;
            case "Helmet":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.Helmet);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
                }
                break;
            case "Necklace":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.Necklace);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
                }
                break;
            case "Ring":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.Ring);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
                }
                break;
            case "shoes":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    //得到数据
                    equipDatasList = EquipDataMgr.Instance.GetEquipDataByType(EquipType.shoes);
                    //跟新面版数据
                    storePanalView.Init(equipDatasList, OnClickBuy);
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
                //在每个面板关闭的时候保存改变的数据
                EquipDataModel.Instance.SaveData();
                MainPanelModel.Instance.SaveData();

                UIManager.Instance.HidePanel("StorePanel");
                break;
            case "BuyButton":

                if (myLocalEquipData == null)
                {
                    Debug.Log("没有选择装备");
                    return;
                }
                //减金币
                if (MainPanelModel.Instance.GetPlayerData().coin >= myLocalEquipData.price)
                {
                    MainPanelModel.Instance.GetPlayerData().coin -= myLocalEquipData.price;
                    EventCenter.Instance.EventTrigger<int>(E_Event_Type.TaskOver, 9002);
                }
                else
                {
                    Debug.Log("钱不够");
                    return;
                }
                //MainPanelController main =UIManager.Instance.GetPanel<MainPanelController>("MainPanel");
                //给自己的背包增加装备
                EquipDataModel.Instance.AddEquip(EquipDataModel.Instance.Local2MyEquipData(myLocalEquipData));
                //更新主面板显示信息
                UIManager.Instance.GetPanel<MainPanelController>("MainPanel").UpdateView();
                break;
            default:
                break;
            
        }
    }

    public void OnClickBuy(LocalEquipData equipData)
    {
        myLocalEquipData = equipData;
        storePanalView.ShowPrice(equipData.price);
    }

}
