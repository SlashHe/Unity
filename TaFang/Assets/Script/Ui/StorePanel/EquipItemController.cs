using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

enum EquipViewType
{
    Bag =0,
    Store =1,
}


public delegate void EquipItemCallback(LocalEquipData equipData);
public delegate void EquipItemCallback2(EquipData equipData);
public class EquipItemController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    public Image equipImage;
    private LocalEquipData myEquipData=null;

    private EquipData myEquipData2 = null;

    public Button click;
    Transform equipItemContent;

    EquipViewType nowType;


    EquipItemCallback callback;

    EquipItemCallback2 callback2;
    //商店的初始化
    public void Init(LocalEquipData equipData, EquipItemCallback callback=null)
    {
        nowType = EquipViewType.Store;
        click.onClick.RemoveAllListeners();
        this.callback = callback;
        click.onClick.AddListener(Click);
        myEquipData = equipData;
        SpriteAtlas sa = ResManager.Instance.Load<SpriteAtlas>("UI/equip/EquipAtlas");
        equipImage.sprite = sa.GetSprite(equipData.icon);
    }
    //背包的初始化
    public void Init(EquipData equipData, EquipItemCallback2 callback = null)
    {
        nowType = EquipViewType.Bag;
        click.onClick.RemoveAllListeners();
        this.callback2 = callback;
        click.onClick.AddListener(Click2);
        myEquipData2 = equipData;
        SpriteAtlas sa = ResManager.Instance.Load<SpriteAtlas>("UI/equip/EquipAtlas");
        equipImage.sprite = sa.GetSprite(equipData.icon);


    }

  


    public void OnPointerEnter(PointerEventData eventData)
    {

        GameObject desModel = PoolManager.Instance.GetObj("Prafebs/UI/EquipDetailsPanel");
        UIManager.Instance.ShowPanel<EquipItemView>("EquipDetailsPanel", (obj) =>
        {
            switch (nowType)
            {
                case EquipViewType.Bag:
                    LocalEquipData temp = EquipDataModel.Instance.MyEquipData2Local(myEquipData2);
                    obj.Init(temp);
                    break;
                case EquipViewType.Store:
                    obj.Init(myEquipData);
                    break;
            }

            Vector3 localPos = GetPosition(gameObject.transform.localPosition, gameObject.transform);
            obj.gameObject.transform.localPosition = new Vector3(localPos.x + 210, localPos.y, localPos.z);
            //obj.gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + equipItemContent.transform.localPosition.y);
        }, E_UI_Layer.Top);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("移除");
        UIManager.Instance.HidePanel("EquipDetailsPanel");
    }

    /// <summary>
    /// 获取相对于canv的相对坐标
    /// </summary>
    /// <param name="local"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Vector3 GetPosition(Vector3 local,Transform obj)
    {
        if (obj.transform.parent != null)
        {
            local = new Vector3(obj.transform.parent.localPosition.x + local.x,
            obj.transform.parent.localPosition.y + local.y, obj.transform.parent.localPosition.z + local.z);
            obj = obj.transform.parent;

            return GetPosition(local, obj);
        }
        else {
            return local;
        }
        
    }

    public void Click()
    {
        if (callback == null)
        {
            return;
        }
        callback(myEquipData);
    }

    public void Click2()
    {
        if (callback2 == null)
        {
            return;
        }
        callback2(myEquipData2);
    }


}
