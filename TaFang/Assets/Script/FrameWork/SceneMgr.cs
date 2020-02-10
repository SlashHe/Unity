using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneMgr : BaseManager<SceneMgr> {


    public void LoadScene(string name)
    {
        MonoMgr.Instance.Mono.StartCoroutine(ReallyLoadScene(name));
    }

    IEnumerator ReallyLoadScene(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);

        while (!async.isDone)
        {
            //更新进度
            EventCenter.Instance.EventTrigger<float>(E_Event_Type.E_Scene_Loading,async.progress);
            yield return 0;
        }

        //告诉加载完了
        EventCenter.Instance.EventTrigger(E_Event_Type.E_Scene_LoadOver, name);


    }
}
