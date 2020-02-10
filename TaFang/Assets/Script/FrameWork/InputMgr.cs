using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 整体输入管理
/// </summary>
public class InputMgr : BaseManager<InputMgr> {

    private bool isOpen = false;

    public InputMgr()
    {
        MonoMgr.Instance.Start();
        EventCenter.Instance.AddEventListener(E_Event_Type.E_Update, Update);
        EventCenter.Instance.AddEventListener(E_Event_Type.E_LateUpdate, LateUpdate);
    }

    public void StartCheck()
    {
        isOpen = true;
    }

    public void StopCheck()
    {
        isOpen = false;
    }


    RaycastHit info;
    Vector3 moveDir;
    void Update () 
	{
        if (!isOpen)
            return;

        //地板射线检测
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info, 100))
        {
            EventCenter.Instance.EventTrigger<Vector3>(E_Event_Type.E_MouseRaycast, info.point);
        }

        //键盘 WASD输入
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        EventCenter.Instance.EventTrigger<Vector3>(E_Event_Type.E_MoveKey, moveDir);

        //鼠标左键点击
        if (Input.GetMouseButtonDown(0))
            EventCenter.Instance.EventTrigger(E_Event_Type.E_MouseLeftDown);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            EventCenter.Instance.EventTrigger(E_Event_Type.E_Late_MouseLeftDown);
    }
}
