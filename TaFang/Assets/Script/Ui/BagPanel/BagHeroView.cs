using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagHeroView : ViewBasePanel
{
    Text AtkValueText;
    Text HpValueText;
    Text DefValueText;
    Text ManaValueText;
    Image Weapon;
    Image shoes;
    Image Ring;
    Image Helmet;
    Image Armor;
    Image Necklace;
    bool hasWeapon=false, hasUpWeapon;
    bool hasshoes = false, hasUpshoes;
    bool hasRing = false, hasUpRing;
    bool hasHelmet = false, hasUpHelmet;
    bool hasArmor = false, hasUpArmor;
    bool hasNecklace = false, hasUpNecklace;


    protected override void Awake()
    {
        base.Awake();
        AtkValueText = GetControl<Text>("AtkValueText");
        HpValueText = GetControl<Text>("HpValueText");
        DefValueText = GetControl<Text>("DefValueText");
        ManaValueText = GetControl<Text>("ManaValueText");
        Weapon = GetControl<Image>("Weapon");
        shoes = GetControl<Image>("shoes");
        Ring = GetControl<Image>("Ring");
        Helmet = GetControl<Image>("Helmet");
        Armor = GetControl<Image>("Armor");
        Necklace = GetControl<Image>("Necklace");
    }

    /// <summary>
    /// 初始化面板，以及更新面板
    /// </summary>
    /// <param name="equips"></param>
    public void Init(List<EquipData> equips,EquipItemCallback2 callback = null)
    {
        int atk=0,hp = 0, def = 0, mana = 0;
        //更新的时候把是否更新置为false
        hasUpWeapon = false;
        hasUpshoes = false;
        hasUpRing = false;
        hasUpHelmet = false;
        hasUpArmor = false;
        hasUpNecklace = false;
        for (int i = 0; i < equips.Count; i++)
        {
            atk += equips[i].addAtk;
            hp += equips[i].addHp;
            def += equips[i].addDef;
            mana += equips[i].addMana;
            //如果有对应装备实列化到面板上
            ShowEquipBar(equips[i], callback);
        }
        //如果没有移除
        RemoveEquip(Armor,ref hasArmor, hasUpArmor);
        RemoveEquip(Helmet, ref hasHelmet, hasUpHelmet);
        RemoveEquip(Necklace, ref hasNecklace, hasUpNecklace);
        RemoveEquip(Ring, ref hasRing, hasUpRing);
        RemoveEquip(shoes, ref hasshoes, hasUpshoes);
        RemoveEquip(Weapon, ref hasWeapon, hasUpWeapon);

        AtkValueText.text = atk.ToString();
        HpValueText.text = hp.ToString();
        DefValueText.text = def.ToString();
        ManaValueText.text = mana.ToString();
    }

    public void ShowEquipBar(EquipData equip, EquipItemCallback2 callback = null)
    {
        switch (equip.equipType)
        {
            case EquipType.Armor:
                UpdateEquip(Armor, out hasArmor, out hasUpArmor, equip, callback);
                break;
            case EquipType.Helmet:
                UpdateEquip(Helmet, out hasHelmet, out hasUpHelmet, equip, callback);
                break;
            case EquipType.Necklace:
                UpdateEquip(Necklace, out hasNecklace, out hasUpNecklace, equip, callback);
                break;
            case EquipType.Ring:
                UpdateEquip(Ring, out hasRing, out hasUpRing, equip, callback);
                break;
            case EquipType.shoes:
                UpdateEquip(shoes, out hasshoes, out hasUpshoes, equip, callback);
                break;
            case EquipType.weapon:
                UpdateEquip(Weapon, out hasWeapon, out hasUpWeapon, equip, callback);
                break;
        }
    }

    public void UpdateEquip(Image equipImage,out bool hasEquip, out bool hasUpEquip, EquipData equip, EquipItemCallback2 callback = null)
    {
        GameObject item = null;
        if (equipImage.transform.childCount < 2)
        {
            //实例化到场景中
            item = PoolManager.Instance.GetObj("Prafebs/UI/equipItem");
            //设置父对象
            item.transform.SetParent(equipImage.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
        }
        else
        {
            item = equipImage.transform.GetChild(1).gameObject;
        }
        EquipItemController si = item.GetComponent<EquipItemController>();
        si.Init(equip, callback);
        hasEquip = true;
        hasUpEquip = true;
    }

    public void RemoveEquip(Image equipImage,ref bool hasEquip, bool hasUpEquip)
    {
        if (hasEquip == true && hasUpEquip == false)
        {
            PoolManager.Instance.PushObj(equipImage.transform.GetChild(1).gameObject);
            hasEquip = false;
        }
    }
}