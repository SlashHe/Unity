using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskItemController : MonoBehaviour {

    public Text taskText;
    public Image rewardImage;
    public Text rewardValueText;
    public Text expValue;
    public Button click;
    public Image TaskItemColor;

    UnityAction<TaskData> myAction;

    private TaskData myTaskData;

    public void Init(TaskData taskData,UnityAction<TaskData> action) {

        //保存当前任务信息
        myTaskData = taskData;
        click.onClick.RemoveAllListeners();
        myAction = action;
        click.onClick.AddListener(Click);
        //跟新图标信息
        //任务介绍
        taskText.text = taskData.des;
        //奖励图标
        switch (taskData.awardType)
        {
            case AwardType.AddCoin:
                Sprite imag = ResManager.Instance.Load<Sprite>("UI/Denglu/jinbi");
                rewardImage.sprite = imag;
                break;
            case AwardType.AddDiamond:
                Sprite imag2 = ResManager.Instance.Load<Sprite>("UI/Main/Icon.L.Resource.4");
                rewardImage.sprite = imag2;
                break;
        }
        //奖励额
        rewardValueText.text = taskData.awardValue.ToString();
        //经验奖励
        expValue.text = taskData.expValue.ToString();
        switch (taskData.taskState)
        {
            case TaskState.isOver:
                TaskItemColor.color = Color.green;
                break;
            case TaskState.notOver:
                TaskItemColor.color = Color.white;
                break;
        }

    }
    public void Click()
    {
        myAction(myTaskData);
    }
}