using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanelController : CtrlBasePanel {

    private List<EquipData> equipDatasList;

    BagHeroView bagHeroView;
    BagListView bagListView;
    DownButtonView downButtonView;

    EquipData selectEquip;
    int clickNum=0;

    EquipType currentType = EquipType.weapon;

    //出售按钮
    public GameObject Button;

    //脱下按钮
    public GameObject BarButton;


    protected override void Awake()
    {
        base.Awake();
        equipDatasList = EquipDataModel.Instance.GetEquipListByType(EquipType.weapon);
        bagHeroView = GetView<BagHeroView>("HeroDetails");
        bagListView = GetView<BagListView>("BagList");
        downButtonView = GetView<DownButtonView>("DownButton");

    }

    void Start()
    {
        Init();
        Button.SetActive(false);
        BarButton.SetActive(false);
    }

    public void Init()
    {

        bagListView.Init(equipDatasList, OpenButton);
        bagHeroView.Init(EquipBarModel.Instance.GetBarList(), OpendBarButton);
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Close":
                //保存数据
                EquipDataModel.Instance.SaveData();
                MainPanelModel.Instance.SaveData();
                EquipBarModel.Instance.SaveData();
                UIManager.Instance.HidePanel("BagPanel");

                break;

                //穿装备
            case "BuyButton":
                //增加装备到装备栏
                EquipBarModel.Instance.AddEquip(selectEquip);
                //刷新英雄面板
                bagHeroView.Init(EquipBarModel.Instance.GetBarList(), OpendBarButton);
                //刷新背包面板
                UpdateBarViewByEquipType(equipDatasList, currentType);
                Button.SetActive(false);



                break;
            case "SellButton":
                //卖掉装备
                //增加金币
                MainPanelModel.Instance.GetPlayerData().coin += selectEquip.price;
                //移除装备
                EquipDataModel.Instance.ReMoveEquip(selectEquip);
                //刷新面板
                UIManager.Instance.GetPanel<MainPanelController>("MainPanel").UpdateView();
                UpdateBarViewByEquipType(equipDatasList, currentType);



                break;
            case "OutButton":
                EquipBarModel.Instance.ReMoveEquip(selectEquip);
                //脱下装备
                bagHeroView.Init(EquipBarModel.Instance.GetBarList(), OpendBarButton);
                //刷新背包面板
                UpdateBarViewByEquipType(equipDatasList, currentType);
                BarButton.SetActive(false);

                break;
            default:
                break;
            
        }
    }

    protected override void onValueChanged(string btnName, bool b)
    {
        switch (btnName)
        {
            case "Weapon":
                if (b)
                {
                    currentType = EquipType.weapon;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                }

                break;
            case "Armor":

                if (b)
                {

                    currentType = EquipType.Armor;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                }
                break;
            case "Helmet":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    currentType = EquipType.Helmet;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                }
                break;
            case "Necklace":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    currentType = EquipType.Necklace;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                }
                break;
            case "Ring":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    currentType = EquipType.Ring;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                }
                break;
            case "shoes":
                Debug.Log("AllToggle" + b);
                if (b)
                {
                    currentType = EquipType.shoes;
                    UpdateBarViewByEquipType(equipDatasList, currentType);
                    
                }
                break;
            default:

                break;
        }
    }

    public void OpenButton(EquipData equipData)
    {
        //把装备值传回来
        //显示按钮
        if (BarButton.activeSelf == true)
        {
            BarButton.SetActive(false);
        }

        if (selectEquip== equipData)
        {
            
            Button.SetActive(false);

            return;
        }
        else
        {
            
            Button.SetActive(true);
        }
        selectEquip = equipData;
        downButtonView.Init(equipData);
    }

    public void OpendBarButton(EquipData equipData) {
        if (Button.activeSelf == true) {
            Button.SetActive(false);
        }
        BarButton.SetActive(true);
        selectEquip = equipData;
    }


    public void UpdateBarViewByEquipType(List<EquipData> equipDatasList,EquipType equipType)
    {
       
        equipDatasList = EquipDataModel.Instance.GetEquipListByType(equipType);
        bagListView.Init(equipDatasList, OpenButton);
    }
}
