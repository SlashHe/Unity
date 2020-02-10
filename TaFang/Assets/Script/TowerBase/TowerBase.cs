using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Mgic,
    Arow,
    Mgic2,
    Arow2,
}

public class TowerInfo
{
    public float atkDistance;
    public float atk;
    public float cdTime;
    public TowerType towerType;

    public TowerInfo(float atkDistance, float atk, float cdTime, TowerType towerType)
    {
        this.atkDistance = atkDistance;
        this.atk = atk;
        this.cdTime = cdTime;
        this.towerType = towerType;
    }
}

public class TowerBase : MonoBehaviour {

    public TowerInfo towerInfo;
    protected float timer;
    protected Collider currentTarget;
    protected Collider[] Target;

    public TowerBaseInfo towerBaseInfo;

    protected virtual void OnEnable()
    {
        timer = 0;

        //towerInfo = new TowerInfo(3, 30, 1);

    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    protected virtual void  Update () {
        timer += Time.deltaTime;

        if (currentTarget == null)
        {
            FindTarget();
        }
        if(currentTarget != null)
        {
            Atk(currentTarget);
        }
	}

    public virtual void FindTarget()
    {
        //查找Player目标
        Target = Physics.OverlapSphere(transform.position, towerInfo.atkDistance, 1 << LayerMask.NameToLayer("Monster"));
        if (Target.Length < 1)
        {

            return;
        }
        //获取最近的目标，
        float min = towerInfo.atkDistance;
        for (int i = 0; i < Target.Length; i++)
        {
            if (min > Vector3.Distance(transform.position, Target[i].transform.position))           
            {
                min = Vector3.Distance(transform.position, Target[i].transform.position);
                currentTarget = Target[i];
            }
        }
        //如果当前目标的阻挡怪物数达到上限，那就没有目标return；

        if (currentTarget == null)
        {
            return;
        }
        //有目标
        //当前目标增加

        //找到目标停止

        Target = null;


    }

    public virtual void Atk(Collider target)
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);


        
    }

    public virtual void RemoveEffect()
    {

    }
}
