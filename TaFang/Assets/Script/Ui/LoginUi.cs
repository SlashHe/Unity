using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUi : BasePanel {

   
    Image progress;
    float process;

    // Use this for initialization
    void Start () {
        EventCenter.Instance.AddEventListener<float>(E_Event_Type.E_Scene_Loading, GetProcess);
        EventCenter.Instance.AddEventListener<string>(E_Event_Type.E_Scene_LoadOver, ClosePanel);
        progress = GetControl<Image>("Logining");
        progress.fillAmount = 0;
        process = 0;

    }
	
	// Update is called once per frame
	void Update () {

        progress.fillAmount = process;
        
	}

    public void GetProcess(float p)
    {
        process = p;
    }

    public void ClosePanel(string name)
    {
        UIManager.Instance.HidePanel("LoginingPanel");
    }
}
