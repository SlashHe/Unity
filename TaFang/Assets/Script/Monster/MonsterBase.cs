using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterInfo : ObjInfo
{

    public float fllowDisdance;
    public float coin;
    public MonsterInfo(float hp, float atk,float speed,float atkDisdance, float fllowDisdance,float coin) :base(hp, atk, speed,atkDisdance)
    {
        this.fllowDisdance = fllowDisdance;
        this.coin = coin;
    }

}

public class MonsterBase : BaseController{

    Transform nowTarget;
    public MonsterInfo monsterInfo;

    //血条
    GameObject canva;
    public GameObject slider;
    public Slider mySlider;
    //路径
    public List<Transform> myRoad;

    public Transform damagpos;

    Collider[] Target;
    Collider currentTarget;
    Transform area;

    int roadIndex;
    bool hasTarget;
    bool isLive;
    public void OnEnable()
    {
        canva = GameObject.Find("Canvas");
        // mySlider.transform.Find("Fill Area").gameObject.SetActive(true);
        myAgent.enabled = true;
        isMove = true;
        monsterInfo = new MonsterInfo(300, 10, 0.4f, 1, 4,10);
        isLive = true;
        hasTarget = false;
        myAgent.speed = monsterInfo.speed;

        slider = PoolManager.Instance.GetObj("Prafebs/Monster/MonsSlider");
        mySlider = slider.GetComponent<Slider>();
        mySlider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(60, 130, 0);
        mySlider.transform.rotation = Quaternion.identity;
        mySlider.transform.SetParent(canva.transform);
        mySlider.value = 1;
        if (area != null)
        {
            area.gameObject.SetActive(true);
        }

        roadIndex = 0;
    }

    public override void Awake()
    {
        base.Awake();
        

    }

    void Start () {
        

    }
	

	void Update () {


        //找到目标后，播放攻击动画，暂停寻路，进行攻击

        if (isLive)
        {
            if (isMove == true)
            {
                PlayAnimator("MonsterInfo", AnimatorType.Run);
                Move();
            }
            //寻找目标
            if (currentTarget == null)
            {
                FindTarget();
            }
            if (hasTarget == true)
            {
                Atk(currentTarget);

            }
           

        }
        //血量
        mySlider.value = monsterInfo.hp / monsterInfo.maxHp;
        mySlider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(60, 130, 0);
        if (monsterInfo.hp <= 0)
        {
            area = mySlider.transform.Find("Fill Area");
            area.gameObject.SetActive(false);
        }
        //移动

    }

    public void FindTarget()
    {
        //查找Player目标
        Target = Physics.OverlapSphere(transform.position, monsterInfo.atkDisdance, 1 << LayerMask.NameToLayer("Player"));
        if(Target.Length<1)
        {
            
            return;
        }
        //获取最近的目标，并且目标阻挡怪物没有上限
        float min = monsterInfo.atkDisdance;
        for (int i = 0; i < Target.Length; i++)
        {
            if(min> Vector3.Distance(transform.position, Target[i].transform.position)&&
                Target[i].GetComponent<PlayerController>().playerInfo.curMonsCount < Target[i].GetComponent<PlayerController>().playerInfo.maxMonsCount)
            {
                min = Vector3.Distance(transform.position, Target[i].transform.position);
                currentTarget = Target[i];
            }
        }
        //如果当前目标的阻挡怪物数达到上限，那就没有目标return；
        
        if(currentTarget == null)
        {
            return;
        }
        //有目标
        //当前目标增加
        currentTarget.GetComponent<PlayerController>().playerInfo.curMonsCount++;
        currentTarget.GetComponent<PlayerController>().monsterList.Add(this.gameObject);
        //找到目标停止
        isMove = false;
        Target = null;
        hasTarget = true;
        
        
    }

    public override void Move() {

        //if (myRoad.Count > 0)
        if(roadIndex< myRoad.Count)
        {
            myAgent.SetDestination(myRoad[roadIndex].position);

            if (Vector3.Distance(transform.position, myRoad[roadIndex].position) < 0.3)
            {
                //myRoad.RemoveAt(0);
                roadIndex++;
            }

        }
  
    }

    public void Atk(Collider target)
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);

        myAgent.SetDestination(target.transform.position);
        //寻找目标
        if (dis < monsterInfo.atkDisdance)
        {


            //到目标面前播放攻击动画
            PlayAnimator("MonsterInfo", AnimatorType.Atk);
            //调用受伤函数
        }
        if (dis >= monsterInfo.atkDisdance&& dis <= monsterInfo.fllowDisdance)
        {
            
            //目标不在攻击范围
            PlayAnimator("MonsterInfo", AnimatorType.Run);

        }
        if(dis > monsterInfo.fllowDisdance)
        {
            //移除目标
            currentTarget.GetComponent<PlayerController>().playerInfo.curMonsCount--;
            currentTarget.GetComponent<PlayerController>().monsterList.Remove(gameObject);
            currentTarget = null;
            hasTarget = false;
            //走路
            isMove = true;
            PlayAnimator("MonsterInfo", AnimatorType.Run);
            //移除攻击目标
            
        }

    }

    public void Damage()
    {
        EventCenter.Instance.EventTrigger<float>(E_Event_Type.E_Monster_Atk, monsterInfo.atk);
    }

    public void OnDamage(float atk)
    {
        monsterInfo.hp -= atk;
        if (monsterInfo.hp <= 0)
        {
            //monsterInfo.hp = 0;
            EventCenter.Instance.EventTrigger<float>(E_Event_Type.MonsterDeath, monsterInfo.coin);
            if (currentTarget != null)
            {
                currentTarget.GetComponent<PlayerController>().PlayAnimator("PlayerInfo", AnimatorType.Idle);
                currentTarget.GetComponent<PlayerController>().playerInfo.curMonsCount--;
                currentTarget.GetComponent<PlayerController>().monsterList.Remove(gameObject);
            }            
            //player.currentTarget = null;
            myAgent.enabled = false;
            currentTarget = null;
            isLive = false;

            hasTarget = false;
            PlayAnimator("MonsterInfo", AnimatorType.Death);
        }
    }

    

    public void Death()
    {
        PoolManager.Instance.PushObj(slider);
        PoolManager.Instance.PushObj(gameObject);
    }

    public void GameOver()
    {
        EventCenter.Instance.EventTrigger(E_Event_Type.EndGame);
    }
}
