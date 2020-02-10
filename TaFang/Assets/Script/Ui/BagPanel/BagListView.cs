using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagListView : ViewBasePanel {

    public Transform equipItemContent;

    

    protected override void Awake()
    {
        base.Awake();
        
    }

    public void Init(List<EquipData> equipDataList, EquipItemCallback2 callback = null)
    {

        ShowEquip(equipDataList, callback);
        

    }


    public void ShowEquip(List<EquipData> equipDataList, EquipItemCallback2 callback = null)
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
            LocalEquipData localEquip = EquipDataModel.Instance.MyEquipData2Local(equipDataList[i]);
            si.Init(equipDataList[i], callback);

        }
        int num = equipItemContent.childCount;
        for (int i = equipDataList.Count; i < num; i++)
        {
            PoolManager.Instance.PushObj(equipItemContent.GetChild(equipDataList.Count).gameObject);

        }
    }
}
