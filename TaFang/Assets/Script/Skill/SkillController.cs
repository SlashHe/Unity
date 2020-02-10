using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * 0.1f);
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("触发");
            other.gameObject.GetComponent<MonsterBase>().OnDamage(40);
            //生成特效
            GameObject effect = PoolManager.Instance.GetObj("Prafebs/Skill/vfx_Muzzle_SpinBlue");
            effect.transform.position = other.transform.position;
            effect.transform.rotation = other.transform.rotation;
        }
    }

}
