using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 作为一个可以提供给 没有继承mono类的对象 使用mono类中的方法 的存在
/// </summary>
public class MonoController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EventCenter.Instance.AddEventListener<int>(E_Event_Type.TaskOver, OverTask);
    }

    void Update () 
	{
        EventCenter.Instance.EventTrigger(E_Event_Type.E_Update);
    }
    private void LateUpdate()
    {
        EventCenter.Instance.EventTrigger(E_Event_Type.E_LateUpdate);
    }

    public void OverTask(int id)
    {
        TaskData task = TaskModel.Instance.GetTaskById(id);
        if (task != null) {
            task.taskState = TaskState.isOver;
        }
        
    }
}
