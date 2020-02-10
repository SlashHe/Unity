using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class EquipItemView : ViewBasePanel {
    Text EquipNameText;
    Text EquipPropertyText;
    Text EquipValueText;
    Text EquipDesText;
    Image skillimage;

    protected override void Awake()
    {
        base.Awake();
        EquipNameText = GetControl<Text>("EquipNameText");
        EquipPropertyText = GetControl<Text>("EquipPropertyText");
        EquipValueText = GetControl<Text>("EquipValueText");
        EquipDesText = GetControl<Text>("EquipDesText");
        skillimage = GetControl<Image>("skillimage");

    }

    public void Init(LocalEquipData equipData)
    {
        string addType="";
        int ValueText = 0;
        switch (equipData.equipType)
        {
            case EquipType.Armor:
                addType = "增加防御:";
                ValueText = equipData.addDef;
                break;
            case EquipType.Helmet:
                addType = "增加血量:";
                ValueText = equipData.addHp;
                break;
            case EquipType.Necklace:
                addType = "增加魔法:";
                ValueText = equipData.addMana;
                break;
            case EquipType.Ring:
                addType = "增加血量:";
                ValueText = equipData.addHp;
                break;
            case EquipType.shoes:
                addType = "增加移速:";
                ValueText = equipData.addSpeed;
                break;
            case EquipType.weapon:
                addType = "增加攻击:";
                ValueText = equipData.addAtk;
                break;
        }
        
        EquipNameText.text = equipData.name;
        EquipPropertyText.text = addType;
        EquipValueText.text = ValueText.ToString();
        EquipDesText.text = equipData.des;
        SpriteAtlas sa = ResManager.Instance.Load<SpriteAtlas>("UI/equip/EquipAtlas");
        skillimage.sprite = sa.GetSprite(equipData.icon);


    }



}
