using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgicTower : TowerBase
{
    public GameObject towerGun;
    public GameObject gunPos;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    public override void Atk(Collider target)
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);
        //寻找目标
        if (dis < towerInfo.atkDistance)
        {
            if (timer > towerInfo.cdTime&& currentTarget.GetComponent<MonsterBase>().monsterInfo.hp>0)
            {
                //攻击目标
                GameObject towerGunMod = PoolManager.Instance.GetObj(towerGun);
                towerGunMod.transform.position = gunPos.transform.position;
                towerGunMod.transform.rotation = Quaternion.identity;
                towerGunMod.GetComponent<GunController>().target = target.gameObject;
                towerGunMod.GetComponent<GunController>().atk = towerInfo.atk;
                timer = 0;
            }
            if(currentTarget.GetComponent<MonsterBase>().monsterInfo.hp <= 0)
            {
                currentTarget = null;
            }


        }
        if (dis >= towerInfo.atkDistance)
        {
            currentTarget = null;
            //目标不在攻击范围
        }

    }

    public override void FindTarget()
    {
        base.FindTarget();
    }
}
