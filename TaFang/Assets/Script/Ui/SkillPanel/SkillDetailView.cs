using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SkillDetailView : ViewBasePanel {

    private Text skillNameText;
    private Image sillImage;
    private Text SkillAtkText;
    private Text SkilManaText;
    private Text SkillCdText;
    private Text SkillDesText;
    private Text DiamondCostText;

    public Button LearnSkillButton;

    
    bool isHave = false;
    protected override void Awake()
    {
        base.Awake();
        
        skillNameText = GetControl<Text>("skillNameText");
        sillImage = GetControl<Image>("sillImage");
        SkillAtkText = GetControl<Text>("SkillAtkText");
        SkilManaText = GetControl<Text>("SkilManaText");
        SkillCdText = GetControl<Text>("SkillCdText");
        SkillDesText = GetControl<Text>("SkillDesText");
        DiamondCostText = GetControl<Text>("DiamondCostText");
    }

    public void Init(LocalSkillData skillData,List<SkillData> myskillDatas)
    {

        SkillData newSkill = new SkillData(skillData.id, skillData.name, skillData.des,
            skillData.atk, skillData.cd, skillData.icon, skillData.price, skillData.mana);
        //注册点击事件
        LearnSkillButton.onClick.RemoveAllListeners();
        LearnSkillButton.onClick.AddListener(() => {
            LearnSkill(newSkill, skillData.price);
        });

        //图集获取数据
        SpriteAtlas sa = ResManager.Instance.Load<SpriteAtlas>("UI/Skill/SkillAtlas");
        sillImage.sprite = sa.GetSprite(skillData.icon);
        skillNameText.text = skillData.name;
        SkillAtkText.text = skillData.atk.ToString();
        SkilManaText.text = skillData.mana.ToString();
        SkillCdText.text = skillData.cd.ToString();
        SkillDesText.text = skillData.des;
        DiamondCostText.text = skillData.price.ToString();

        //控制是否显示学习技能
        //如果拥有该技能那么不弹
        //如果没有该技能就谈
        
        for (int i = 0; i < myskillDatas.Count; i++)
        {
            if(skillData.id== myskillDatas[i].id)
            {
                isHave = true;
                break;
            }
            if(i== myskillDatas.Count - 1)
            {
                isHave =false;
            }
        }
        if (isHave==true)
        {
            LearnSkillButton.gameObject.SetActive(false);
        }
        else
        {
            LearnSkillButton.gameObject.SetActive(true);
        }
 
    }

    public void LearnSkill(SkillData newSkill,int price)
    {
        //可以买
        if(MainPanelModel.Instance.GetPlayerData().diamond>= price)
        {
            MainPanelModel.Instance.GetPlayerData().diamond -= price;
        }
        else
        {
            Debug.Log("钱不够");
            return;
        }      
        SkillDataModel.Instance.AddSkill(newSkill);
        UIManager.Instance.GetPanel<MainPanelController>("MainPanel").UpdateView();
        UIManager.Instance.GetPanel<SkillPanelController>("SkillPanel").UpdateView();
        gameObject.SetActive(false);

    }
    
}

