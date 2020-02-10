using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListView : ViewBasePanel {


    public Transform skillItemContent;

    public void Init(List<SkillData> skillDatas)
    {
        ShowerSkills(skillDatas);
    }


    public void ShowerSkills(List<SkillData> skillDatas)
    {
        if(skillDatas == null)
        {
            return;
        }

        for (int i = 0; i < skillDatas.Count; i++)
        {
            GameObject item = null;
            if (i < skillItemContent.childCount)
            {
                item = skillItemContent.GetChild(i).gameObject;

            }
            else
            {
                item = PoolManager.Instance.GetObj("Prafebs/UI/skillItem");
                item.transform.SetParent(skillItemContent);
                (item.transform as RectTransform).localScale = Vector3.one;
            }
            
            SkillItemController si = item.GetComponent<SkillItemController>();
            si.Init(skillDatas[i]);
        }

        int num = skillItemContent.childCount;
        for (int i = skillDatas.Count; i < num; i++)
        {
            PoolManager.Instance.PushObj(skillItemContent.GetChild(skillDatas.Count).gameObject);

        }

    }
}
