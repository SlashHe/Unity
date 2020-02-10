using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deastroy : MonoBehaviour {
    public float disTime;

    private void OnEnable()
    {
        Invoke("DeastroyMe", disTime);
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DeastroyMe()
    {
        PoolManager.Instance.PushObj(gameObject);
    }
}
