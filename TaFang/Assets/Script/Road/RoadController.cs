using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoadController : MonoBehaviour {
    
    //路径
    public  List<Transform> road;

    public Transform bornPoint;

    //怪物类型
    public List<GameObject> monsterList;

    //每只怪的间隔时间
    float timer;

    //自定义波数
    public int waves;

    //对应波数的怪物数量
    public int monsNum;

    int nowMonsNum;
    //当前波数
    int nowWave;

    float waitTime;

    GameObject canva;
    void Start () {
        canva = GameObject.Find("Canvas");
        nowWave = 0;
        nowMonsNum = 0;
        waitTime = 0;
    }


	void Update () {
        timer += Time.deltaTime;
        waitTime += Time.deltaTime;



    }
    private void LateUpdate()
    {
        if (timer > 2&& waitTime>12)
        {
            timer = 0;
            CreadMonster();

        }
    }

    public void CreadMonster()
    {
        if(nowWave< waves-1)
        {
            GameObject monster = PoolManager.Instance.GetObj(monsterList[nowWave]);
            monster.transform.position = bornPoint.position;
            monster.transform.rotation = bornPoint.rotation;
            MonsterBase monsCtr = monster.GetComponent<MonsterBase>();
            MonsterInfo mons = new MonsterInfo(300* (nowWave+1), 10, 0.4f * (nowWave + 1), 1, 4, 20 * (nowWave + 1));
            monsCtr.monsterInfo = mons;
            for (int i = 0; i < road.Count; i++)
            {
                monsCtr.myRoad.Add(road[i]);
            }
            nowMonsNum++;
            if (nowMonsNum >= monsNum)
            {
                nowMonsNum = 0;
                nowWave += 1;
            }
        }
        if(nowWave == waves - 1) {

            GameObject monster = PoolManager.Instance.GetObj(monsterList[nowWave]);
            monster.transform.position = bornPoint.position;
            monster.transform.rotation = bornPoint.rotation;
            MonsterBase monsCtr = monster.GetComponent<MonsterBase>();
            MonsterInfo mons = new MonsterInfo(4000, 50, 0.5f, 0, 0, 1000);
            monsCtr.monsterInfo = mons;
            for (int i = 0; i < road.Count; i++)
            {
                monsCtr.myRoad.Add(road[i]);
            }
            nowWave += 1;
        }
        
    }
}
