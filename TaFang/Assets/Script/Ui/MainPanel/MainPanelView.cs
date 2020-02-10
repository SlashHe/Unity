using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView : ViewBasePanel {

    private Text coinText;
    private Text diamondText;
    private Text powerText;
    private Text expText;
    private Image exp_bar;
    private Text nameText;
    private Text lvText;

    protected override void Awake()
    {
        base.Awake();
        coinText = GetControl<Text>("CoinText");
        diamondText = GetControl<Text>("DiamondText");
        powerText = GetControl<Text>("PowerText");
        expText = GetControl<Text>("expText");
        exp_bar = GetControl<Image>("exp_bar");
        nameText = GetControl<Text>("nameText");
        lvText = GetControl<Text>("lvText");
    }

    protected void Start()
    {
        
    }
    //数据初始化
    public void Init(PlayerData playerData)
    {
        coinText.text = playerData.coin.ToString();
        diamondText.text = playerData.diamond.ToString();
        powerText.text = string.Format("{0}/80", playerData.power);
        expText.text = string.Format("exp:{0}/100", playerData.Exp);
        exp_bar.fillAmount = playerData.Exp / 100;
        nameText.text = playerData.name;
        lvText.text = playerData.lv.ToString();
    }
	
}
