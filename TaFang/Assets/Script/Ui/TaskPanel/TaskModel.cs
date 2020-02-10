using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum TaskState {
    
    isOver=0,
    notOver=1,

}

public class ServerTaskData {

    public string username;
    public int id;
    public TaskState taskState;

    public ServerTaskData() { }

    public ServerTaskData(string username,int id, TaskState taskState) {

        this.username = username;
        this.id = id;
        this.taskState = taskState;

    }
}

public class TaskData {

    public int id;
    public string des;
    public AwardType awardType;
    public TaskType taskType;
    public int expValue;
    public TaskState taskState;
    public int awardValue;

    public TaskData() { }

    public TaskData(int id, TaskState taskState, string des, AwardType awardType, TaskType taskType, int expValue, int awardValue) {

        this.id = id;
        this.taskState = taskState;
        this.des = des;
        this.awardType = awardType;
        this.taskType = taskType;
        this.expValue = expValue;
        this.awardValue = awardValue;
    }

}


public class TaskModel : BaseManager<TaskModel> {

    string userName = "";

    //根据任务类型存任务
    private Dictionary<TaskType, List<TaskData>> taskDic = new Dictionary<TaskType, List<TaskData>>();


    //总表信息，存所有服务器数据
    private List<ServerTaskData> serverTaskList = null;


    /// <summary>
    /// 初始化服务器数据
    /// </summary>
    public void InitServerData() {

        //找当当前的用户名
        if (userName == "")
        {
            userName = UserModel.Instance.GetMyUserId();
        }

        serverTaskList = new List<ServerTaskData>();

        //如果没有初始数据，就创建一个用户初始数据
        if (!File.Exists(Application.persistentDataPath + "/TaskData.Json"))
        {
            //没有服务器数据就先初始化数据
            InitData(userName, 9001, TaskState.notOver);
            InitData(userName, 9002, TaskState.notOver);
            InitData(userName, 9003, TaskState.notOver);
            InitData(userName, 9004, TaskState.notOver);
            InitData(userName, 9005, TaskState.notOver);
            InitData(userName, 9006, TaskState.notOver);

            
        }
        else
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/TaskData.Json");
            //读Json
            serverTaskList = JsonMapper.ToObject<List<ServerTaskData>>(json);
            //取出表里面当前玩家的数据，存在当前玩家的服务器数据中
            for (int i = 0; i < serverTaskList.Count; i++)
            {
                if (serverTaskList[i].username == userName)
                {
                    //根据服务器数据到本地获取数据
                    LocalTaskData localTaskModel = LocalTaskModel.Instance.GetLocalTaskDataById(serverTaskList[i].id);
                    //合并数据
                    TaskData taskData = new TaskData(localTaskModel.id, serverTaskList[i].taskState, localTaskModel.des,
                        localTaskModel.awardType, localTaskModel.taskType, localTaskModel.expValue, localTaskModel.awardValue);

                    //存到字典中
                    AddTask(taskData);
                }
            }
            //如果新用户没有服务器数据就先初始化数据
            if (taskDic.Count == 0)
            {
                InitData(userName, 9001, TaskState.notOver);
                InitData(userName, 9002, TaskState.notOver);
                InitData(userName, 9003, TaskState.notOver);
                InitData(userName, 9004, TaskState.notOver);
                InitData(userName, 9005, TaskState.notOver);
                InitData(userName, 9006, TaskState.notOver);
            }

        }

        //读入json
        SaveData();
    }

    public List<TaskData> GetTaskListByType(TaskType taskType)
    {
        //如果没有就先初始化
        if (serverTaskList == null)
        {
            InitServerData();
        }
        return taskDic[taskType];
    }

    public TaskData GetTaskById(int id) {

        //如果没有就先初始化
        if (serverTaskList == null)
        {
            InitServerData();
        }
        foreach (var item in taskDic)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                if (item.Value[i].id == id) {
                    return item.Value[i];
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 保存服务器数据
    /// </summary>
    public void SaveData() {
        if (serverTaskList == null) {
            InitServerData();
        }
        for (int i = serverTaskList.Count - 1; i >= 0; i--)
        {
            if (serverTaskList[i].username == userName)
            {
                serverTaskList.Remove(serverTaskList[i]);
            }
        }
        foreach (var item in taskDic)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                ServerTaskData serverTaskData = new ServerTaskData(userName, item.Value[i].id, item.Value[i].taskState);
                serverTaskList.Add(serverTaskData);
            }
        }
        string json = JsonMapper.ToJson(serverTaskList);
        File.WriteAllText(Application.persistentDataPath + "/TaskData.Json", json, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="id"></param>
    /// <param name="taskState"></param>
    public void InitData(string userName,int id, TaskState taskState) {
        ServerTaskData serverTaskData = new ServerTaskData(userName, id, taskState);
        serverTaskList.Add(serverTaskData);

        //根据服务器数据到本地获取数据
        LocalTaskData localTaskModel = LocalTaskModel.Instance.GetLocalTaskDataById(serverTaskData.id);
        //合并数据
        TaskData taskData = new TaskData(localTaskModel.id, serverTaskData.taskState, localTaskModel.des,
            localTaskModel.awardType, localTaskModel.taskType, localTaskModel.expValue, localTaskModel.awardValue);

        //存到字典中
        AddTask(taskData);


    }

    /// <summary>
    /// 增加任务
    /// </summary>
    /// <param name="taskData"></param>
    public void AddTask(TaskData taskData) {

        if (taskDic.ContainsKey(taskData.taskType))
        {
            taskDic[taskData.taskType].Add(taskData);
        }
        else
        {
            taskDic.Add(taskData.taskType, new List<TaskData>());
            taskDic[taskData.taskType].Add(taskData);
        }

    }

    /// <summary>
    /// 移除任务
    /// </summary>
    /// <param name="taskData"></param>
    public void RemoveTask(TaskData taskData) {
        if (taskDic.ContainsKey(taskData.taskType))
        {
            taskDic[taskData.taskType].Remove(taskData);
        }
    }
}
