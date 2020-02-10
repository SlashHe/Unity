using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Controller : MonoBehaviour {

    Vector3 pos;
    float timer = 0;
    Collider[] other;
    // Use this for initialization
    private void OnEnable()
    {
        
    }
    void Start () {
        pos = transform.position - new Vector3(0, 5, 0);
        GameObject.Destroy(gameObject, 3f);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
        other = Physics.OverlapSphere(transform.position, 1, 1<<LayerMask.NameToLayer("Monster"));
        if (timer > 0.5)
        {
            for (int i = 0; i < other.Length; i++)
            {
                Debug.Log(other[i].name);
                other[i].gameObject.GetComponent<MonsterBase>().OnDamage(40);
                //生成特效
                GameObject effect = PoolManager.Instance.GetObj("Prafebs/Skill/FireHit");
                effect.transform.position = other[i].transform.position;
                effect.transform.rotation = other[i].transform.rotation;
                
            }
            timer = 0;
        }
           
    }

  

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Monster")
    //    {
    //        if (timer > 0.3)
    //        {
    //            other.gameObject.GetComponent<MonsterBase>().OnDamage(40);
    //            //生成特效
    //            GameObject effect = PoolManager.Instance.GetObj("Prafebs/Skill/FireHit");
    //            effect.transform.position = other.transform.position;
    //            effect.transform.rotation = other.transform.rotation;
    //            timer = 0;
    //        }
            
    //    }
    //}
}
