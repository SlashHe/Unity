using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPanelController : CtrlBasePanel {

    SkillListView skillListView;

    HeroPanelView heroPanelView;

    protected override void Awake()
    {
        base.Awake();
        skillListView = GetView<SkillListView>("SkillPlane");
        heroPanelView = GetView<HeroPanelView>("DesBg");
    }

    private void Start()
    {
        ShowSkill();
        UpdataHeroInfo();
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {

            case "Close":
                UIManager.Instance.HidePanel("HeroPanel");
                break;
            default:
                break;
        }
    }

    public void ShowSkill()
    {
        skillListView.Init(SkillDataModel.Instance.GetSkillList());
    }

    public void UpdataHeroInfo()
    {
        heroPanelView.Init(HeroModel.Instance.GetHeroData());
    }
}
