using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelView : ViewBasePanel {

    private Image skillMask1;
    private Image skillMask2;
    private Image skillMask3;
    private Image skillMask4;
    private Image skillMask5;
    private Image skillMask6;
    private Image skillMask7;
    private Image skillMask8;

    private Dictionary<int, Image> skillImageDic = new Dictionary<int, Image>();

    protected override void Awake()
    {
        base.Awake();
        skillMask1 = GetControl<Image>("skillMask1");
        skillMask2 = GetControl<Image>("skillMask2");
        skillMask3 = GetControl<Image>("skillMask3");
        skillMask4 = GetControl<Image>("skillMask4");
        skillMask5 = GetControl<Image>("skillMask5");
        skillMask6 = GetControl<Image>("skillMask6");
        skillMask7 = GetControl<Image>("skillMask7");
        skillMask8 = GetControl<Image>("skillMask8");
        skillImageDic.Add(2011,skillMask1);
        skillImageDic.Add(2009,skillMask2);
        skillImageDic.Add(2015,skillMask3);
        skillImageDic.Add(2017,skillMask4);
        skillImageDic.Add(2020,skillMask5);
        skillImageDic.Add(2026,skillMask6);
        skillImageDic.Add(2027,skillMask7);
        skillImageDic.Add(2029,skillMask8);
    }

    public void Init(List<SkillData> mySkill)
    {
        for (int i = 0; i < mySkill.Count; i++)
        {
            if (skillImageDic.ContainsKey(mySkill[i].id))
            {
                skillImageDic[mySkill[i].id].color = new Color(skillImageDic[mySkill[i].id].color.r,
                    skillImageDic[mySkill[i].id].color.g, skillImageDic[mySkill[i].id].color.b,0); 
            }
        }
    }

}
