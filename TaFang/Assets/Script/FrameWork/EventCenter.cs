using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;

    public EventInfo(UnityAction<T> a)
    {
        action = a;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction action;

    public EventInfo(UnityAction a)
    {
        action = a;
    }
}


public enum E_Event_Type
{
    E_Input_KeyDown,

    E_Scene_Loading,

    E_Scene_LoadOver,

    //LateUpdate
    E_LateUpdate,


    //更新
    E_Update,
    //射线
    E_MouseRaycast,
    //wasd键
    E_MoveKey,

    //鼠标左键按下
    E_Late_MouseLeftDown,

    //鼠标左键按下
    E_MouseLeftDown,
    //怪物到达
    E_Monster_Arrive,

    //怪物数量改变
    E_Monster_Change,
    //波数通知
    E_Wave_Notice,
    //怪物攻击
    E_Monster_Atk,
    //玩家攻击
    E_Player_Atk,

    //怪物死亡
    MonsterDeath,

    //游戏结束
    EndGame,

    //任务完成
    TaskOver,
}

/// <summary>
/// 时间中心——观察者设计模式
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //容器
    //private Dictionary<E_Event_Type, UnityAction<T>> eventDic = new Dictionary<E_Event_Type, UnityAction<T>>();
    private Dictionary<E_Event_Type, IEventInfo> eventDic = new Dictionary<E_Event_Type, IEventInfo>();
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fun"></param>
    //public void AddEventListener( E_Event_Type type, UnityAction<object> fun )
    public void AddEventListener<T>(E_Event_Type type, UnityAction<T> fun)
    {
        //if (eventDic.ContainsKey(type))
        //    eventDic[type] += fun;
        //else
        //    eventDic.Add(type, fun);
        if (eventDic.ContainsKey(type))
            (eventDic[type] as EventInfo<T>).action += fun;
        else
            eventDic.Add(type, new EventInfo<T>(fun));
    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fun"></param>
    //public void AddEventListener( E_Event_Type type, UnityAction<object> fun )
    public void AddEventListener(E_Event_Type type, UnityAction fun)
    {
        if (eventDic.ContainsKey(type))
            (eventDic[type] as EventInfo).action += fun;
        else
            eventDic.Add(type, new EventInfo(fun));
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fun"></param>
    public void RemoveEventListener<T>(E_Event_Type type, UnityAction<T> fun)
    {
        if (eventDic.ContainsKey(type))
            (eventDic[type] as EventInfo<T>).action -= fun;
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fun"></param>
    public void RemoveEventListener(E_Event_Type type, UnityAction fun)
    {
        if (eventDic.ContainsKey(type))
            (eventDic[type] as EventInfo).action -= fun;
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="info"></param>
    public void EventTrigger<T>( E_Event_Type type, T info )
    {
        if (eventDic.ContainsKey(type))
        {
            if((eventDic[type] as EventInfo<T>).action != null)
                (eventDic[type] as EventInfo<T>).action(info);
        }
            
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="info"></param>
    public void EventTrigger(E_Event_Type type)
    {
        if (eventDic.ContainsKey(type))
        {
            if ((eventDic[type] as EventInfo).action != null)
                (eventDic[type] as EventInfo).action();
        }

    }

    /// <summary>
    /// 清空指定事件的所有监听
    /// </summary>
    /// <param name="type"></param>
    public void Clear(E_Event_Type type)
    {
        if (eventDic.ContainsKey(type))
        {
            eventDic.Remove(type);
        }
    }

    /// <summary>
    /// 清空所有
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
