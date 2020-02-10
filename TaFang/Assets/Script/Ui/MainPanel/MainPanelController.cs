using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : CtrlBasePanel {

    bool isActive;
    public GameObject buttonGroup;
    private PlayerData myPlayerData;
    private MainPanelView mainPanelView;

    protected override void Awake()
    {
        base.Awake();
        mainPanelView = GetView<MainPanelView>("UpView");
        isActive = true;
        myPlayerData = MainPanelModel.Instance.GetPlayerData();
    }

    //面板打开就先初始化
    private void Start()
    {
        UpdateView();
    }

    /// <summary>
    /// 更新主面板数据
    /// </summary>
    public void UpdateView()
    {
        mainPanelView.Init(myPlayerData);
    }

    //点击事件
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "ListButton":
                if (isActive)
                {

                    buttonGroup.SetActive(false);
                    isActive = false;
                }
                else if (isActive == false)
                {

                    buttonGroup.SetActive(true);
                    isActive = true;
                }

                break;

            case "HeroButton":
                UIManager.Instance.ShowPanel("GameMain/HeroPanel");
                break;

            case "atk_Button":

                UIManager.Instance.ShowPanel("GameLevel/GameLevelPanel");
                break;
                //任务
            case "taskButton":
                UIManager.Instance.ShowPanel("GameMain/TaskPanel");
                break;
                //技能
            case "SkillButton":
                UIManager.Instance.ShowPanel("GameMain/SkillPanel");
                break;
                //背包
            case "BagButton":
                UIManager.Instance.ShowPanel("GameMain/BagPanel");
                break;
                //商店
            case "StoreButton":
                UIManager.Instance.ShowPanel("GameMain/StorePanel");
                break;
            //设置按钮
            case "Atk_Button":
                UIManager.Instance.ShowPanel("GameLevel/GameLevelPanel");
                break;
            case "SetButton":
                break;
                //图鉴按钮
            case "IllustrationButton":
                break;
            default:
                break;
        }
    }
}
