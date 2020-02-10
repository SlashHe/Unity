using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelController : CtrlBasePanel {

    public GameObject desPanel;
    public Button desClose;
    
    SkillDetailView desPanelView;
    SkillPanelView skillPanelView;


    protected override void Awake()
    {

        base.Awake();
        skillPanelView = GetView<SkillPanelView>("DesBg");
        desPanelView = desPanel.GetComponent<SkillDetailView>();
        desClose.onClick.AddListener(CloseDesPanel);
        
    }

    private void Start()
    {
        UpdateView();
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "skillItem1":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2011), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2011), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem2":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2009), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2009), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem3":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2015), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2015), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem4":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2017), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2017), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem5":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2020), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2020), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem6":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2026), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2026), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem7":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2027), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2027), SkillDataModel.Instance.GetSkillList());
                }
                break;
            case "skillItem8":
                if (desPanel.activeSelf == true)
                {
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2029), SkillDataModel.Instance.GetSkillList());
                }
                else
                {
                    desPanel.SetActive(true);
                    desPanelView.Init(LocalSkillDataMgr.Instance.GetSkillDataById(2029), SkillDataModel.Instance.GetSkillList());
                }
                break;

            case "Close":
                SkillDataModel.Instance.SaveData();
                UIManager.Instance.HidePanel("SkillPanel");
                break;
            default:
                break;
        }
    }

    public void CloseDesPanel()
    {
        desPanel.SetActive(false);
    }

    public void UpdateView()
    {
        skillPanelView.Init(SkillDataModel.Instance.GetSkillList());
    }
}
