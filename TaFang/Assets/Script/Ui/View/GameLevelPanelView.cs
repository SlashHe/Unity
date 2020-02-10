using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelPanelView : BasePanel {

    GameObject DescPlane;
    bool isActive;

    protected override void Awake()
    {
        base.Awake();
        DescPlane = GetControl<Image>("DescPlane").gameObject;
        isActive = false;
        DescPlane.SetActive(false);
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {

            case "ReturnButton":
                UIManager.Instance.HidePanel("GameLevel/GameLevelPanel");
                break;
            case "LevelButton1":
                if (isActive)
                {
                    DescPlane.SetActive(false);
                    isActive = false;
                }
                else if (isActive == false)
                {

                    DescPlane.SetActive(true);
                    isActive = true;
                }
                break;

            case "FightButton":
                UIManager.Instance.ShowPanel<LoginUi>("Logining/LoginingPanel", (obj) => {
                    //更新面板上的数据
                    SceneMgr.Instance.LoadScene("Main");
                    UIManager.Instance.HidePanel("GameLevel/GameLevelPanel");
                    UIManager.Instance.HidePanel("GameMain/MainPanel");
                });
                break;
            default:
                break;
        }
    }
}
