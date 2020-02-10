using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;





public class PlayerInfo : ObjInfo
{
    public bool skill1;
    public bool skill2;
    public float cdTime1;
    public float cdTime2;

    public int maxMonsCount;
    public int curMonsCount;

    public PlayerInfo(float hp, float atk, float speed,float atkDisdance) :base(hp,atk, speed, atkDisdance)
    {
        skill1 = true;
        skill2 = true;
        cdTime1 = 8;
        cdTime2 = 20;
        atkDisdance = 1;
        maxMonsCount = 2;
        curMonsCount = 0;
    }

}


public class PlayerController : BaseController{

    //射线信息
    Ray ray;
    RaycastHit hitInfo;
    //玩家信息
    public PlayerInfo playerInfo;

    //怪物列表
    public List<GameObject> monsterList;
    public GameObject currentTarget;

    //圆圈
    public Transform lan;

    public Transform skillPos;

    public bool isLive;
    public override void Awake()
    {
        lan = gameObject.transform.Find("lan");
        lan.gameObject.SetActive(false);
        isLive = false;
        base.Awake();
        EventCenter.Instance.AddEventListener(E_Event_Type.E_Late_MouseLeftDown,Move);
        EventCenter.Instance.AddEventListener<float>(E_Event_Type.E_Monster_Atk, OnDamage);
        InputMgr.Instance.StartCheck();
        playerInfo = new PlayerInfo(100,60,2,1);
        myAgent.speed = playerInfo.speed;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        


        StopMove();

       

    }

    private void LateUpdate()
    {
        FindTarget();
        if (currentTarget != null)
        {
            
           Atk(currentTarget);
        }
    }

    public override void Move()
    {
        if (isLive == true)
        {
            isMove = true;
            PlayAnimator("PlayerInfo", AnimatorType.Run);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 100);
            myAgent.SetDestination(hitInfo.point);
        }

    }
    public void StopMove()
    {
        if (isMove == true)
        {
            if (Mathf.Abs(myAgent.remainingDistance) < 0.01)
            {

                PlayAnimator("PlayerInfo", AnimatorType.Idle);
                isMove = false;
            }
        }
    }

    public void OnDamage(float atk)
    {
        playerInfo.hp -= atk;
        if (playerInfo.hp <= 0)
        {
            playerInfo.hp = 0;
        }
    }

    public void Atk(GameObject target)
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);

        MonsterBase mons = target.GetComponent<MonsterBase>();
        
        if (dis < playerInfo.atkDisdance)
        {

            //到目标面前播放攻击动画
            if (mons.monsterInfo.hp > 0)
            {
                
                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
                PlayAnimator("PlayerInfo", AnimatorType.Atk);
            }
            else

                currentTarget = null;
            //调用受伤函数
        }
        else
        {
            PlayAnimator("PlayerInfo", AnimatorType.Run);
        }
    }

    public void FindTarget()
    {
        if (monsterList.Count < 1)
        {
            currentTarget = null;
            return;
        }
        
        //获取最近的目标，
        float min = playerInfo.atkDisdance;
        for (int i = 0; i < monsterList.Count; i++)
        {
            MonsterBase mons = monsterList[i].GetComponent<MonsterBase>();
            if (min > Vector3.Distance(transform.position, monsterList[i].transform.position)&& mons.monsterInfo.hp>0)
            {
                min = Vector3.Distance(transform.position, monsterList[i].transform.position);
                currentTarget = monsterList[i];
            }
            
            
        }

        //移除死亡怪物
        for (int i = 0; i < monsterList.Count; i++)
        {
            MonsterBase mons = monsterList[i].GetComponent<MonsterBase>();
            if (mons.monsterInfo.hp <= 0)
            {
                monsterList.RemoveAt(i);
                return;
            }
        }
        
        //找到目标停止
        //isMove = false;

    }

    public void Damage()
    {
        if (currentTarget != null)
        {
            MonsterBase mons = currentTarget.GetComponent<MonsterBase>();
            mons.OnDamage(playerInfo.atk);
        }
        
    }

    public void ReleaseSkill()
    {
        myAnimator.SetTrigger("Skill1");
    }

    public void ReleaseSkillMod()
    {
        GameObject skillMod = PoolManager.Instance.GetObj("Prafebs/Skill/Sword");
        skillMod.transform.position = skillPos.transform.position;
        skillMod.transform.rotation = transform.rotation;
    }

    
}
