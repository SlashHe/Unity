using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Instance.ShowPanel<LoginPanelController>("Start/StartPlane");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
