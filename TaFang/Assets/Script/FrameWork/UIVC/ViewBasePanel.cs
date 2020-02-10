using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///  能找到所有显示类组件,返回组件GetControl
/// </summary>
public class ViewBasePanel : BasePanel {

    protected override void Awake()
    {
        FindControl<Text>();
        FindControl<Image>();
        FindControl<Slider>();
        FindControl<RawImage>();
        FindControl<ScrollRect>();
        FindControl<InputField>();
    }
}
