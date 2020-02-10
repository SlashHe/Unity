using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskPanelView : ViewBasePanel {

    public Transform FinishTaskContent;
    public Transform UnFinishTaskContent;

    

    public void Init(List<TaskData> taskDatas,UnityAction<TaskData> action) {
        
        if (taskDatas == null)
        {
            return;
        }
        List<TaskData> overTaskList = new List<TaskData>();
        List<TaskData> notOverTaskList= new List<TaskData>();

        //分类未完成和已完成
        for (int i = 0; i < taskDatas.Count; i++)
        {
            switch (taskDatas[i].taskState)
            {
                case TaskState.isOver:
                    overTaskList.Add(taskDatas[i]);
                    break;
                case TaskState.notOver:
                    notOverTaskList.Add(taskDatas[i]);
                    break;
            }
        }

        #region 未完成的任务
        for (int i = 0; i < notOverTaskList.Count; i++)
        {
            GameObject item = null;

            if (i < UnFinishTaskContent.childCount)
            {
                item = UnFinishTaskContent.GetChild(i).gameObject;

            }
            else
            {
                //实例化到场景中
                item = PoolManager.Instance.GetObj("Prafebs/UI/TaskItem");
                //设置父对象
                item.transform.SetParent(UnFinishTaskContent);
                (item.transform as RectTransform).localScale = Vector3.one;

            }
            TaskItemController si = item.GetComponent<TaskItemController>();
            //初始化图标
            si.Init(notOverTaskList[i], action);

        }
        int num2 = UnFinishTaskContent.childCount;
        for (int i = notOverTaskList.Count; i < num2; i++)
        {
            PoolManager.Instance.PushObj(UnFinishTaskContent.GetChild(notOverTaskList.Count).gameObject);
        }
        #endregion

        #region 已完成任务
        for (int i = 0; i < overTaskList.Count; i++)
        {
            GameObject item = null;

            if (i < FinishTaskContent.childCount -1)
            {
                item = FinishTaskContent.GetChild(i+1).gameObject;

            }
            else
            {
                //实例化到场景中
                item = PoolManager.Instance.GetObj("Prafebs/UI/TaskItem");
                //设置父对象
                item.transform.SetParent(FinishTaskContent);
                (item.transform as RectTransform).localScale = Vector3.one;

            }
            TaskItemController si = item.GetComponent<TaskItemController>();
            //初始化图标
            si.Init(overTaskList[i], action);

        }
        int num = FinishTaskContent.childCount - 1;
        for (int i = overTaskList.Count; i < num; i++)
        {
            PoolManager.Instance.PushObj(FinishTaskContent.GetChild(overTaskList.Count+1).gameObject);
        }
        #endregion

        UnFinishTaskContent.SetParent(FinishTaskContent);
        UnFinishTaskContent.SetSiblingIndex(FinishTaskContent.childCount-1);
    }
}
