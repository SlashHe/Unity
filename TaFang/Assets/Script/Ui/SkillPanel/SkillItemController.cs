using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SkillItemController : MonoBehaviour {

    public Image skillImage;
    

    public void Init(SkillData skillData)
    {
        SpriteAtlas sa = ResManager.Instance.Load<SpriteAtlas>("UI/Skill/SkillAtlas");
        skillImage.sprite = sa.GetSprite(skillData.icon);
    }
    
}
