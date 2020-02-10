using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存池管理器
/// </summary>
public class PoolManager : BaseManager<PoolManager>
{
    //容器
    Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();
    Dictionary<string, GameObject> parentDic = new Dictionary<string, GameObject>();
    //取东西（要用的时候在容器中找到然后激活，拿出来用）
    public GameObject GetObj(string name)
    {
        GameObject obj;
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {   //激活         
            obj = poolDic[name][0];
            poolDic[name].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;

        }
        obj.transform.SetParent(null);
        obj.SetActive(true);
        return obj;

    }

    public GameObject GetObj(GameObject name)
    {
        GameObject obj;
        if (poolDic.ContainsKey(name.name) && poolDic[name.name].Count > 0)
        {   //激活         
            obj = poolDic[name.name][0];
            poolDic[name.name].RemoveAt(0);
        }
        else
        {

            obj = GameObject.Instantiate(name);
            obj.name = name.name;

        }
        obj.transform.SetParent(null);
        obj.SetActive(true);
        return obj;

    }

    //放东西
    public void PushObj(GameObject obj)
    {

        obj.SetActive(false);
        //失活物体放入缓存池
        if (poolDic.ContainsKey(obj.name))
        {
            poolDic[obj.name].Add(obj);
        }
        else
        {
            poolDic.Add(obj.name, new List<GameObject>() { obj });
        }
        //把所有失活的游戏物体放入一个指定的对象中
        if (!parentDic.ContainsKey(obj.name))
        {
            GameObject pObj = new GameObject();
            pObj.name = obj.name + "Pool";
            parentDic.Add(obj.name, pObj);

        }
        obj.transform.SetParent(parentDic[obj.name].transform);

    }
    public void Clear()
    {
        parentDic.Clear();
        poolDic.Clear();
    }

}
