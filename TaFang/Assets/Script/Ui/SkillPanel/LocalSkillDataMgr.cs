using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 本地技能数据
/// </summary>
public class LocalSkillData
{
    public int id;
    public string name;
    public string des;
    public int atk;
    public int cd;
    public string icon;
    public int price;
    public int mana;

}

/// <summary>
/// 存放本地数据
/// </summary>
public class LocalSkillDataMgr : BaseManager<LocalSkillDataMgr> {

    private Dictionary<int, LocalSkillData> localSkillDataDic = new Dictionary<int, LocalSkillData>();
    //初始化数据库
    private void Init()
    {
        //从本地读取数据
        TextAsset json = Resources.Load<TextAsset>("Config/SkillData");
        List<LocalSkillData> localSkills = JsonMapper.ToObject<List<LocalSkillData>>(json.text);
        for (int i = 0; i < localSkills.Count; i++)
        {
            localSkillDataDic.Add(localSkills[i].id, localSkills[i]);
        }
       
    }

    public LocalSkillData GetSkillDataById(int id)
    {
        //获取数据的时候初始化
        if(localSkillDataDic.Count == 0)
        {
            Init();
        }
        if (localSkillDataDic.ContainsKey(id))
        {
            return localSkillDataDic[id];
        }
        else
        {
            return null;
        }
    }

	
}
