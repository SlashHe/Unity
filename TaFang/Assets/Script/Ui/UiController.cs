using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiController : MonoBehaviour {

    public Text coinNum;
    public ToggleGroup group;
    public GameMgr gameMgr;

    // Use this for initialization
    void Start () {
        gameMgr = GameObject.Find("Main Camera (1)").GetComponent<GameMgr>();
        coinNum.text = string.Format("" + gameMgr.gameInfo.coin);

    }
	
	// Update is called once per frame
	void Update () {
        coinNum.text = string.Format(""+gameMgr.gameInfo.coin);

    }

    public void Toggle(bool b)
    {
        Debug.Log(b);
        if (!b)
        {
            return;
        }
        
    }
}
