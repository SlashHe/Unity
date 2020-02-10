using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController mono = null;


    public MonoMgr()
    {
        if (mono == null)
        {
            GameObject obj = new GameObject();
            obj.name = "Mono";
            mono = obj.AddComponent<MonoController>();
        }
    }

    public MonoController Mono
    {
        get
        {
            return mono;
        }
    }
	

    public void Start()
    {

    }
}
