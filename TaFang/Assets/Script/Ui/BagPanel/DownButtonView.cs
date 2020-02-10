using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownButtonView : ViewBasePanel {

    Text DiamondCostText;

    protected override void Awake()
    {
        base.Awake();
        DiamondCostText = GetControl<Text>("DiamondCostText");

    }

    public void Init(EquipData equipData)
    {
        DiamondCostText.text = equipData.price.ToString();
    }
}
