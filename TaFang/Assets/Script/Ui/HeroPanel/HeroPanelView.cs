using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanelView : ViewBasePanel {

    Text HeroLvText;
    Text HeroAtkText;
    Text HeroHpText;
    Text HeroManaText;
    Text HeroDefText;
    Text HeroSpeedText;
    Text HeroDesText;

    protected override void Awake()
    {
        base.Awake();
        HeroLvText = GetControl<Text>("HeroLvText");
        HeroAtkText = GetControl<Text>("HeroAtkText");
        HeroHpText = GetControl<Text>("HeroHpText");
        HeroManaText = GetControl<Text>("HeroManaText");
        HeroDefText = GetControl<Text>("HeroDefText");
        HeroSpeedText = GetControl<Text>("HeroSpeedText");
        HeroDesText = GetControl<Text>("HeroDesText");
    }



    public void Init(HeroData heroData)
    {
        HeroLvText.text = heroData.lv.ToString();
        HeroAtkText.text = heroData.atk.ToString();
        HeroHpText.text = heroData.hp.ToString();
        HeroManaText.text = heroData.mana.ToString();
        HeroDefText.text = heroData.def.ToString();
        HeroSpeedText.text = heroData.speed.ToString();
        HeroDesText.text = heroData.des.ToString();

    }
}
