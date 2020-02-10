using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AnimatorType
{

    Idle = 0,
    Run = 1,
    Atk = 2,
    Wound = 3,
    Death=4,
    Skill=5,

}

public class ObjInfo
{
    public float hp;
    public float maxHp;
    public float atk;
    public float atkDisdance;
    //移动速度
    public float speed;

    public ObjInfo(float hp, float atk, float speed,float atkDisdance)
    {
        this.hp = hp;
        this.atk = atk;
        this.speed = speed;
        this.maxHp = hp;
        this.atkDisdance = atkDisdance;
    }

}

public class BaseController : MonoBehaviour {

    //寻路
    protected NavMeshAgent myAgent;
    //动画
    protected Animator myAnimator;
    //移动状态
    public bool isMove = false;

    public virtual void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Move() { }

    public virtual void Atk() { }

    public virtual void PlayAnimator(string paramName, AnimatorType type)
    {
        myAnimator.SetInteger(paramName, (int)type);
    }

}
