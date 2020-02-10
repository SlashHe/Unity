using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI面板基类
///1.帮我们找到所有的控件 方便子类使用
///2.提供一些共性方法
/// </summary>
public class BasePanel : MonoBehaviour {

    private Dictionary<string, List<UIBehaviour>> controlsDic = new Dictionary<string, List<UIBehaviour>>();


    protected virtual void Awake()
    {
        //去找到所有的控件
        FindControl<Button>();
        FindControl<Text>();
        FindControl<Image>();
        FindControl<Toggle>();
        FindControl<RawImage>();
        FindControl<Slider>();
        FindControl<ScrollRect>();
        FindControl<InputField>();
        
    }

    /// <summary>
    /// 传入Button组件的名字，
    /// </summary>
    /// <param name="btnName"></param>
    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void onValueChanged(string btnName, bool b)
    {

    }

    public void click() { }
    /// <summary>
    /// 寻找控件，传入T：你想要找的控件
    /// </summary>
    /// <typeparam name="T">T控件的类型</typeparam>
    protected void FindControl<T>() where T:UIBehaviour
    {
        //返回子物体的所有T组件
        T[] btns = this.GetComponentsInChildren<T>();
        //组件所在游戏物体的名字
        string controlName;
        for (int i = 0; i < btns.Length; ++i)
        {
            //获得名字
            controlName = btns[i].gameObject.name;
            //找到控件，依次对应名字存到字典中
            if (controlsDic.ContainsKey(controlName))
                controlsDic[controlName].Add(btns[i]);
            else
                controlsDic.Add(controlName, new List<UIBehaviour>() { btns[i] });


            //遍历的同时注册对应组件的事件，根据名字来传递
            //如果是Button         
            if(btns[i] is Button)
            {
                string btName = btns[i].gameObject.name;
                //注册事件,分析第一层：AddListener里面需要注册一个执行的函数
                //每个按钮按下都会执行这个函数
                //所以我们通过他的名字来识别，对应的btName应该执行哪个方法块(Switch)
                //->决定再封装一层委托把名字传进去
                //总结：需要传入不同的参数就再加一层
                //注意：匿名函数的简写是lambda表达式，他们不是委托，只是一个简写的方法体

                
                (btns[i] as Button).onClick.AddListener(()=> {
                    OnClick(btName);
                });

                //(btns[i] as Button).onClick.AddListener(delegate {
                //    OnClick(btName);
                //});
            }            

            else if(btns[i] is Toggle)
            {
                string cName = btns[i].gameObject.name;
                (btns[i] as Toggle).onValueChanged.AddListener(delegate(bool b) {
                    onValueChanged(cName, b);
                });
                
            }
        }
    }

    /// <summary>
    /// 根据控件名 和你想找的 控件类型 来找到控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        if (controlsDic.ContainsKey(name))
        {
            List<UIBehaviour> cList = controlsDic[name];
            for (int i = 0; i < cList.Count; i++)
            {
                if(cList[i] is T)
                {
                    return cList[i] as T;
                }
            }
        }
        return null;
    }
}
