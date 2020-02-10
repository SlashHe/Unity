using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlaneView : ViewBasePanel
{


    //
    List<ServerItem> serverItemList = new List<ServerItem>();
 
    //服务器图标
    //private GameObject serverItem;
    //图标所要放到的父物体
    public Transform serverItemContent;

    SelectCallback selectServerCallback;
   

    //面板的初始化
    public void Init(SelectCallback callback, List<ServerData> serverDataList)
    {
        
        selectServerCallback = callback;
        
        ShowServer(serverDataList);

    }

    /// <summary>
    /// 显示所有的服务器
    /// </summary>
    public void ShowServer(List<ServerData> serverDataList)
    {

        //1.如果当前图标数小于传进来的数据那么就添加
        //2.如果大于就失活
        if (serverDataList == null)
        {
            return;
        }

        Debug.Log("更新");
        for (int i = 0; i < serverDataList.Count; i++)  
        {
            GameObject item = null;

            if (i < serverItemContent.childCount) 
            {
                item = serverItemContent.GetChild(i).gameObject;

            }
            else
            {
                //实例化到场景中
                item = PoolManager.Instance.GetObj("Prafebs/UI/ServerItem");
                //设置父对象
                item.transform.SetParent(serverItemContent);
                (item.transform as RectTransform).localScale = Vector3.one;
                
            }
            ServerItem si = item.GetComponent<ServerItem>();
            //初始化图标
            si.Init(serverDataList[i], selectServerCallback);
 
        }
        int num = serverItemContent.childCount;
        for (int i = serverDataList.Count; i < num; i++)
        {
            PoolManager.Instance.PushObj(serverItemContent.GetChild(serverDataList.Count).gameObject);
            
        }
    }



}
