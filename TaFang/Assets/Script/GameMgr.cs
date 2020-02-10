using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInfo
{
    public float coin;
    public float hp;

    public GameInfo()
    {
        coin = 100;
        hp = 90;
        
    }
}

public class GameMgr : MonoBehaviour {
    public GameInfo gameInfo;

    public GameObject endGame;
    Ray ray;
    RaycastHit hitInfo;
    GameObject player;
    GameObject currentTowerBase=null;
    GameObject currentTower=null;
    GameObject magic;
    GameObject area;
    GameObject range;
    public GameObject BaseChoice;
    public GameObject TowerChoice;
    bool isOpendBaseChoice;
    bool isOpendClik;
    bool isOpendSkill;

    bool isPlayer;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("liubei");
        BaseChoice.SetActive(false);
        TowerChoice.SetActive(false);
        isOpendBaseChoice = false;
        isOpendClik = true;
        gameInfo = new GameInfo();
        isPlayer = false;
        isOpendSkill = false;
        EventCenter.Instance.AddEventListener<float>(E_Event_Type.MonsterDeath, AddCoin);

        EventCenter.Instance.AddEventListener(E_Event_Type.EndGame, EndGame);
    }
	
	// Update is called once per frame
	void Update () {
        if (isOpendClik)
        {

            Clik();
        }

        if (isOpendSkill)
        {
            SkillClick();

        }
        opendRange();

    }

    public void opendRange()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo, 100, 1 << LayerMask.NameToLayer("Tower")))
        {
            if (range == null)
            {
                range = PoolManager.Instance.GetObj("Prafebs/Skill/range");
            }
            range.transform.position = hitInfo.transform.position;
        }
        else
        {
            if (range != null)
            {
                PoolManager.Instance.PushObj(range);
            }
            range = null;
        }
    }

    public void Clik()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 100);
            //点击到角色

            Debug.Log(hitInfo.transform.tag);
            if (hitInfo.transform.tag == "Player"&& isPlayer==false)
            {
                Debug.Log("kai");
                opendPlayer();
                isPlayer = true;
                return;
            }

            if (hitInfo.transform.tag == "Player" && isPlayer == true)
            {
                Debug.Log("guan");
                closePlayer();
                isPlayer = false;
                return;
            }
            //点击到防御塔基座

            if (hitInfo.transform.tag == "TowerBase" && currentTowerBase == null)
            {
                
                //记录当前选中的塔基座
                currentTowerBase = hitInfo.transform.gameObject;
                if(currentTowerBase.GetComponent<TowerBaseInfo>().isCread == true)
                {
                    return;
                }

                //创建选中特效
                magic = PoolManager.Instance.GetObj("Prafebs/TowerBase/MagicChoice");
                magic.transform.position = currentTowerBase.transform.position;
                magic.transform.rotation = currentTowerBase.transform.rotation;
                //关闭玩家选中
                closePlayer();
                //弹出UI
                opendBaseChoice();
                //关闭检测
                isOpendClik = false;

            }

            //点击到塔防
            else if (hitInfo.transform.tag == "Tower" && currentTower == null)
            {
                
                //记录当前选中的塔
                currentTower = hitInfo.transform.gameObject;
                magic = PoolManager.Instance.GetObj("Prafebs/TowerBase/MagicChoice");
                magic.transform.position = currentTower.transform.position;
                magic.transform.rotation = currentTower.transform.rotation;
                //关闭玩家选中
                closePlayer();
                //弹出UI
                opendTowerChoice();
                //关闭检测
                isOpendClik = false;
            }

            //else
            //{
            //    closeBaseChoice();

            //}
        }
    }

    public void opendPlayer()
    {
        player.GetComponent<PlayerController>().isLive = true;
        player.GetComponent<PlayerController>().lan.gameObject.SetActive(true);
    }

    public void closePlayer()
    {
        player.GetComponent<PlayerController>().isLive = false;
        player.GetComponent<PlayerController>().lan.gameObject.SetActive(false);
    }

    public void opendTowerChoice()
    {
        TowerChoice.SetActive(true);
    }

    public void closeTowerChoice()
    {
        currentTower = null;
        TowerChoice.SetActive(false);
        //关闭面板开启检测
        isOpendClik = true;
        //关闭特效

        PoolManager.Instance.PushObj(magic);
    }

    public void opendBaseChoice()
    {

        BaseChoice.SetActive(true);
    }

    public void closeBaseChoice()
    {
        currentTowerBase = null;
        currentTower = null;
        BaseChoice.SetActive(false);
        //关闭面板开启检测
        isOpendClik = true;
        //关闭特效
        
        PoolManager.Instance.PushObj(magic);
    }


    public void AddCoin(float coin)
    {
        gameInfo.coin += coin;
    }
    public void CreadArowTower()
    {
        if (gameInfo.coin >= 30)
        {
            gameInfo.coin -= 30;

            GameObject tower = PoolManager.Instance.GetObj("Prafebs/TowerGun/Tower_Arow");
            tower.transform.position = currentTowerBase.transform.position;
            tower.transform.rotation = currentTowerBase.transform.rotation;
            //塔的属性
            tower.GetComponent<TowerBase>().towerInfo = new TowerInfo(3, 30, 1,TowerType.Arow);
            //状态已经创建
            tower.GetComponent<TowerBase>().towerBaseInfo = currentTowerBase.GetComponent<TowerBaseInfo>();
            currentTowerBase.GetComponent<TowerBaseInfo>().isCread = true;
            closeBaseChoice();
        }
           
    }

    public void CreadMgicTower()
    {
        if (gameInfo.coin >= 30)
        {
            gameInfo.coin -= 30;
            GameObject tower = PoolManager.Instance.GetObj("Prafebs/TowerGun/Tower_Mgic");
            tower.transform.position = currentTowerBase.transform.position;
            tower.transform.rotation = currentTowerBase.transform.rotation;
            //塔的属性
            tower.GetComponent<TowerBase>().towerInfo = new TowerInfo(3, 30, 2,TowerType.Mgic);

            tower.GetComponent<TowerBase>().towerBaseInfo = currentTowerBase.GetComponent<TowerBaseInfo>();
            currentTowerBase.GetComponent<TowerBaseInfo>().isCread = true;
            closeBaseChoice();
        }
        
    }

    public void CreadsoldierTower()
    {
        currentTowerBase.GetComponent<TowerBaseInfo>().isCread = true;
        closeBaseChoice();
    }

    public void UpgradeTower()
    {
        if (currentTower.GetComponent<TowerBase>().towerInfo.towerType == TowerType.Arow2||
            currentTower.GetComponent<TowerBase>().towerInfo.towerType == TowerType.Mgic2)
        {
            return;
        }
        if(gameInfo.coin >= 100)
        {
            gameInfo.coin -= 100;
            if (currentTower.GetComponent<TowerBase>().towerInfo.towerType == TowerType.Arow)
            {

                GameObject tower = PoolManager.Instance.GetObj("Prafebs/TowerGun/Tower_Arow2");
                tower.transform.position = currentTower.transform.position;
                tower.transform.rotation = currentTower.transform.rotation;
                //塔的属性
                tower.GetComponent<TowerBase>().towerInfo = new TowerInfo(4, 40, 0.7f, TowerType.Arow2);
                tower.GetComponent<TowerBase>().towerBaseInfo = currentTower.GetComponent<TowerBase>().towerBaseInfo;
                
            }
            if (currentTower.GetComponent<TowerBase>().towerInfo.towerType == TowerType.Mgic)
            {

                GameObject tower = PoolManager.Instance.GetObj("Prafebs/TowerGun/Tower_Mgic2");
                tower.transform.position = currentTower.transform.position;
                tower.transform.rotation = currentTower.transform.rotation;
                //塔的属性
                tower.GetComponent<TowerBase>().towerInfo = new TowerInfo(4, 40, 1f, TowerType.Mgic2);
                tower.GetComponent<TowerBase>().towerBaseInfo = currentTower.GetComponent<TowerBase>().towerBaseInfo;
                
            }
            RemoveTower();
            closeTowerChoice();

        }
       

    }
    //拆除塔
    public void RemoveTower()
    {
        gameInfo.coin += 15;
        currentTower.GetComponent<TowerBase>().towerBaseInfo.isCread = false;
        currentTower.GetComponent<TowerBase>().RemoveEffect();
        PoolManager.Instance.PushObj(currentTower);
        closeTowerChoice();
        //返还金币

    }

    public void StartSkill2()
    {
        //点击按钮，执行这个函数，
        //射线检测鼠标位置，只要是在路上，就生成范围预制体，
        //在次点击释放技能

        isOpendClik = false;
        isOpendSkill = true;

    }

    public void SkillClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo, 100, 1 << LayerMask.NameToLayer("Plane")))
        {
            if(area == null)
            {
                area = PoolManager.Instance.GetObj("Prafebs/Skill/area");
            }
            area.transform.position = hitInfo.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ReleaseSkill2();
            PoolManager.Instance.PushObj(area);
            area = null;
            isOpendSkill = false;
            isOpendClik = true;
        }
    }

    public void ReleaseSkill2()
    {
        GameObject skillMod = PoolManager.Instance.GetObj("Prafebs/Skill/fire");
        skillMod.transform.position = area.transform.position + new Vector3(0,5, 0);
        skillMod.transform.rotation = area.transform.rotation;
        isOpendClik = true;
    }


    public void EndGame()
    {
        endGame.SetActive(true);
        Invoke("ChangeSence", 5);
    }

    public void ChangeSence()
    {
        SceneMgr.Instance.LoadScene("Login");
    }
}
