using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject target;
    public float atk;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.GetComponent<MonsterBase>().damagpos.position, 0.1f);
    }


    public void OnCollisionEnter(Collision collision)
    {
        //当碰到敌人的时候
        //调用敌人受伤函数
        //销毁自己
        Debug.Log("触发");
        if (collision.gameObject.tag == "Monster")
        {
            
            collision.gameObject.GetComponent<MonsterBase>().OnDamage(atk);
            PoolManager.Instance.PushObj(gameObject);
        }

        
    }
}
