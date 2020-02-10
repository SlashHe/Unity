using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_UI_Layer
{
    Bottom,
    Middle,
    Top,
    System,
}

/// <summary>
/// UI管理器
/// 1.管理所有的面板 容器来存储面板
/// 2.提供给外部 面板相关的 公共方法
///     a.显示一个面板
///     b.隐藏一个面板
///     c.得到一个面板
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    //字典存的的是所有面板上的脚本
    public Dictionary<string, BasePanel> dic = new Dictionary<string, BasePanel>();

    //字典存的的是所有面板上的Control脚本
    //public Dictionary<string,CtrlBasePanel> dic = new Dictionary<string, CtrlBasePanel>();

    public Transform bot;
    public Transform mid;
    public Transform top;
    public Transform system;


    public UIManager()
    {
        GameObject obj = ResManager.Instance.Load<GameObject>("UI/Canvas");
        
        bot = obj.transform.Find("Bottom");
        mid = obj.transform.Find("Middle");
        top = obj.transform.Find("Top");
        system = obj.transform.Find("System");
        GameObject.DontDestroyOnLoad(obj);

        GameObject sys = ResManager.Instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(sys);
    }


    /// <summary>
    /// 加载界面，nameUI子目录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">UI子目录</param>
    /// <param name="callBack"></param>
    /// <param name="layer"></param>
    public void ShowPanel<T>(string name, UnityAction<T> callBack = null, E_UI_Layer layer = E_UI_Layer.Middle) where T:BasePanel
    {
        string[] s = name.Split('/');
        string pathName = s[s.Length - 1];
        //有不处理
        if (dic.ContainsKey(name))
            return;

        //没有
        //1.实例化面板
        ResManager.Instance.LoadAsyn<GameObject>("Prafebs/UI/" + name, (GameObject obj) =>
        {
            //加载完成后设置面板的父物体
            Transform father = mid;
            switch(layer)
            {
                case E_UI_Layer.Bottom:
                    father = bot;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            //2.放到canvas下面
            (obj.transform as RectTransform).SetParent(father);
            //缩放大小归一
            obj.transform.localScale = Vector3.one;
            //重置位置
            (obj.transform as RectTransform).offsetMin = Vector3.zero;
            (obj.transform as RectTransform).offsetMax = Vector3.zero;

            dic.Add(pathName, obj.GetComponent<BasePanel>());

            //把当前字典存的代码通过委托发给你
            if(callBack != null)
                callBack(dic[pathName] as T);
        });

        
    }
    /// <summary>
    /// 加载界面，name:Prafebs/UI/子目录
    /// </summary>
    /// <param name="name">UI子目录</param>
    /// <param name="layer"></param>
    public void ShowPanel(string name,  E_UI_Layer layer = E_UI_Layer.Middle) 
    {
        //
        string[] s = name.Split('/');
        string pathName = s[s.Length - 1];
        //有不处理
        if (dic.ContainsKey(pathName))
            return;

        //没有
        //1.实例化面板
        ResManager.Instance.LoadAsyn<GameObject>("Prafebs/UI/" + name, (obj) =>
        {
            Transform father = mid;
            switch (layer)
            {
                case E_UI_Layer.Bottom:
                    father = bot;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            //2.放到canvas下面
            (obj.transform as RectTransform).SetParent(father);
            //缩放大小归一
            obj.transform.localScale = Vector3.one;
            //重置位置
            (obj.transform as RectTransform).offsetMin = Vector3.zero;
            (obj.transform as RectTransform).offsetMax = Vector3.zero;

            dic.Add(pathName, obj.GetComponent<BasePanel>());
 
        });
    }

    public void HidePanel(string name)
    {
        if (dic.ContainsKey(name))
        {
            GameObject.Destroy(dic[name].gameObject);
            dic.Remove(name);
        }
    }

    
    /// <summary>
    /// 获取Ctronller的代码(name：Ctronller挂载的父物体的名字)
    /// </summary>
    /// <typeparam name="T">view的类名</typeparam>
    /// <param name="name">view挂载的父物体的名字</param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (dic.ContainsKey(name))
            return dic[name] as T;
        return null;
        
    }

    
}
