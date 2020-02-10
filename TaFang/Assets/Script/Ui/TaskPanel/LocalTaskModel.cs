using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AwardType {
    
    AddCoin=0,
    AddDiamond=1,

}

public enum TaskType {

    MainTask=0,
    SideTask=1,

}
public class LocalTaskData {

    public int id;
    public string des;
    public AwardType awardType;
    public TaskType taskType;
    public int expValue;
    public int awardValue;

    public LocalTaskData() { }

    public LocalTaskData(int id , string des, AwardType awardType, TaskType taskType, int expValue, int awardValue) {

        this.id = id;
        this.des = des;
        this.awardType = awardType;
        this.taskType = taskType;
        this.expValue = expValue;
        this.awardValue = awardValue;
    }
}


public class LocalTaskModel : BaseManager<LocalTaskModel> {

    private Dictionary<int, LocalTaskData> localTaskDic = null;

    private List<LocalTaskData> localTaskDatasList = null;

    private void Init() {
        //从本地读取数据
        TextAsset json = Resources.Load<TextAsset>("Config/TaskData");
        localTaskDatasList = JsonMapper.ToObject<List<LocalTaskData>>(json.text);
        localTaskDic = new Dictionary<int, LocalTaskData>();
        for (int i = 0; i < localTaskDatasList.Count; i++)
        {
            localTaskDic.Add(localTaskDatasList[i].id, localTaskDatasList[i]);
        }
    }

    public LocalTaskData GetLocalTaskDataById(int id) {

        if (localTaskDic == null) {
            Init();
        }
        if (localTaskDic.ContainsKey(id))
        {
            return localTaskDic[id];
        }
        else
        {
            return null;
        }

    }


}
