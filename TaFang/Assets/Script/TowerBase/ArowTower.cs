using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArowTower : TowerBase
{

    public GameObject towerGun;
    public GameObject gunPos;
    public GameObject towerGunMod;
    public GameObject Turret;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

	}

    public override void Atk(Collider target)
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);
        //寻找目标
        if (dis < towerInfo.atkDistance)
        {
            if (timer > towerInfo.cdTime && currentTarget.GetComponent<MonsterBase>().monsterInfo.hp > 0)
            {
                //攻击目标
                if (towerGunMod == null)
                {
                    towerGunMod = PoolManager.Instance.GetObj(towerGun);
                }
                //炮头旋转
                Turret.transform.rotation = Quaternion.Lerp(Turret.transform.rotation, Quaternion.LookRotation((new Vector3(target.transform.position.x, Turret.transform.position.y,
                    target.transform.position.z) - Turret.transform.position), Vector3.up), 0.8f);
                towerGunMod.transform.position = gunPos.transform.position;
                towerGunMod.transform.rotation = gunPos.transform.rotation;

                //生成特效
                GameObject effect = PoolManager.Instance.GetObj("Prafebs/TowerGun/vfx_Hit_ArrowFire_Mobile");
                effect.transform.position = target.transform.position;
                effect.transform.rotation = target.transform.rotation;
                //造成伤害
                currentTarget.GetComponent<MonsterBase>().OnDamage(towerInfo.atk);
                timer = 0;
            }
            if (currentTarget.GetComponent<MonsterBase>().monsterInfo.hp <= 0)
            {
                if (towerGunMod != null)
                {
                    PoolManager.Instance.PushObj(towerGunMod);
                }
                towerGunMod = null;
                currentTarget = null;
            }


        }
        if (dis >= towerInfo.atkDistance)
        {
            if (towerGunMod != null)
            {
                PoolManager.Instance.PushObj(towerGunMod);
            }
            towerGunMod = null;
            currentTarget = null;
            //目标不在攻击范围
        }

    }

    public override void FindTarget()
    {
        base.FindTarget();
    }

    public override void RemoveEffect()
    {
        if (towerGunMod != null)
        {
            PoolManager.Instance.PushObj(towerGunMod);
        }
    }
}
