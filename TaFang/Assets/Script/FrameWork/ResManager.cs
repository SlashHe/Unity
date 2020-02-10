using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源管理器 
/// 1.加载预设体 Resource加载
/// 2.AB包资源加载
/// 
/// 资源加载 应该分为两种 大方式  同步和异步
/// </summary>
public class ResManager : BaseManager<ResManager>
{
    /// <summary>
    /// 同步加载Resource的资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="isAutoCreate"></param>
    /// <returns></returns>
    public T Load<T>(string name, bool isAutoCreate = true) where T : Object
    {
        T obj = Resources.Load<T>(name);
        if (obj is GameObject && isAutoCreate)
            return GameObject.Instantiate(obj);

        return obj;
    }

    /// <summary>
    /// 异步加载 资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callBack"></param>
    public void LoadAsyn<T>(string name, UnityAction<T> callBack) where T : Object
    {
        MonoMgr.Instance.Mono.StartCoroutine(ReallyLoadAsyn(name, callBack));
    }

    /// <summary>
    /// 协程异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    IEnumerator ReallyLoadAsyn<T>(string name, UnityAction<T> callBack, bool isAutoCreate = true) where T : Object
    {
        ResourceRequest req = Resources.LoadAsync<T>(name);
        yield return req;
        //加载完后才执行后面的代码
        if (req.asset is GameObject && isAutoCreate)
            callBack(GameObject.Instantiate<T>(req.asset as T));
        else
            callBack(req.asset as T);

    }
}
