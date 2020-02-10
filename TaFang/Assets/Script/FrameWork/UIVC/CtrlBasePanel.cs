using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller基类，能够找到所有控制类组件以及面板所有view
/// 在start函数中需要Getview的代码
/// </summary>
public class CtrlBasePanel : BasePanel {

    private Dictionary<string, ViewBasePanel> viewDic = new Dictionary<string, ViewBasePanel>();

    protected override void Awake()
    {
        FindControl<Toggle>();
        FindControl<Button>();
        FindControl<Slider>();
        FindView();
    }

    /// <summary>
    /// 找到View
    /// </summary>
    /// <typeparam name="T">view的类名</typeparam>
    //protected void FindView<T>() where T : ViewBasePanel
    //{
    //    T[] views = GetComponentsInChildren<T>();
    //    string viewName;
    //    for (int i = 0; i < views.Length; i++)
    //    {
    //        viewName = views[i].gameObject.name;
    //        viewDic.Add(viewName, views[i]);
    //    }
    //}

    private void FindView() 
    {
        ViewBasePanel[] views = GetComponentsInChildren<ViewBasePanel>();
        string viewName;
        for (int i = 0; i < views.Length; i++)
        {
            viewName = views[i].gameObject.name;
            viewDic.Add(viewName, views[i]);
        }
    }

    /// <summary>
    /// 获取Ctronller这个面板下管理的View的代码(name：view挂载的父物体的名字)
    /// </summary>
    /// <typeparam name="T">view的类名</typeparam>
    /// <param name="name">view挂载的父物体的名字</param>
    /// <returns></returns>
    protected T GetView<T>(string name) where T: ViewBasePanel
    {
        if (viewDic.ContainsKey(name))
        {
            return viewDic[name] as T;
        }
        else return null;
    }

}
