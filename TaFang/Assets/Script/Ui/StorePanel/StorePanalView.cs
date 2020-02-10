using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanalView : ViewBasePanel {

    public Transform equipItemContent;

    private Text coinText;

    protected override void Awake()
    {
        base.Awake();
        coinText = GetControl<Text>("DiamondCostText");
    }

    public void Init(List<LocalEquipData> equipDataList, EquipItemCallback callback = null)
    {

        ShowEquip(equipDataList, callback);
        coinText.text = "";

    }

    public void ShowPrice(int price)
    {
        coinText.text = price.ToString();
    }


    public void ShowEquip(List<LocalEquipData> equipDataList, EquipItemCallback callback=null)
    {
        //1.如果当前图标数小于传进来的数据那么就添加
        //2.如果大于就失活
        if (equipDataList == null)
        {
            return;
        }

        Debug.Log("更新");
        for (int i = 0; i < equipDataList.Count; i++)
        {
            GameObject item = null;

            if (i < equipItemContent.childCount)
            {
                item = equipItemContent.GetChild(i).gameObject;

            }
            else
            {
                //实例化到场景中
                item = PoolManager.Instance.GetObj("Prafebs/UI/equipItem");
                //设置父对象
                item.transform.SetParent(equipItemContent);
                (item.transform as RectTransform).localScale = Vector3.one;

            }
            EquipItemController si = item.GetComponent<EquipItemController>();
            //初始化图标
            si.Init(equipDataList[i], callback);

        }
        int num = equipItemContent.childCount;
        for (int i = equipDataList.Count; i < num; i++)
        {
            PoolManager.Instance.PushObj(equipItemContent.GetChild(equipDataList.Count).gameObject);

        }
    }
}
