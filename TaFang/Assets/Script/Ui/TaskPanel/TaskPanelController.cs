using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanelController : CtrlBasePanel
{
    private TaskPanelView taskPanelView;

    private List<TaskData> taskDataList= new List<TaskData>();

    TaskType currentType = TaskType.MainTask;

    protected override void Awake()
    {
        base.Awake();
        taskPanelView = GetView<TaskPanelView>("RightDes");

    }

    private void Start()
    {
        currentType = TaskType.MainTask;
        UpdateTaskViewByTaskType(taskDataList, currentType);
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Close":
                //保存数据
                MainPanelModel.Instance.SaveData();
                TaskModel.Instance.SaveData();
                UIManager.Instance.HidePanel("TaskPanel");
                break;
            default:
                break;
        }
    }

    protected override void onValueChanged(string btnName, bool b)
    {
        switch (btnName)
        {
            case "MainTask":
                if (b)
                {
                    currentType = TaskType.MainTask;
                    UpdateTaskViewByTaskType(taskDataList, currentType);
                }

                break;
            case "SideTask":

                if (b)
                {
                    currentType = TaskType.SideTask;
                    UpdateTaskViewByTaskType(taskDataList, currentType);
                }
                break;
            default:

                break;
        }
    }

    public void UpdateTaskViewByTaskType(List<TaskData> taskDataList, TaskType taskType)
    {
        taskDataList = TaskModel.Instance.GetTaskListByType(taskType);
        taskPanelView.Init(taskDataList, AddAward);
    }


    public void AddAward(TaskData taskData) {

        //完成任务增加奖励,并且删除任务，刷新主面板
        if (taskData.taskState == TaskState.isOver)
        {
           
            MainPanelModel.Instance.GetPlayerData().Exp += taskData.expValue;
            switch (taskData.awardType)
            {
                case AwardType.AddCoin:
                    MainPanelModel.Instance.GetPlayerData().coin += taskData.awardValue;
                    break;
                case AwardType.AddDiamond:
                    MainPanelModel.Instance.GetPlayerData().diamond += taskData.awardValue;
                    break;
            }
            //刷新主面板
            UIManager.Instance.GetPanel<MainPanelController>("MainPanel").UpdateView();
            //移除任务
            TaskModel.Instance.RemoveTask(taskData);
            //刷新任务面板
            UpdateTaskViewByTaskType(taskDataList, currentType);
        }
        else {
            return;
        }
        
    }

    
    
}
