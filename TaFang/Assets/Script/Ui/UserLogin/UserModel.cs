using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class UserData
{
    public string username;
    public string password;
    public string serverName;
    public UserData(string username,string password)
    {
        this.username = username;
        this.password = password;
    }

    public UserData() { }
}


public class UserModel : BaseManager<UserModel> {

     
    private Dictionary<string, UserData> userDic = new Dictionary<string, UserData>();
    private UserData myUser=new UserData();
    public UserModel() {
        if (!File.Exists(Application.persistentDataPath + "/userData.Json"))
        {

            return;
        }
        else
        {

            List<UserData> userList = new List<UserData>();
            //读取数据
            string json = File.ReadAllText(Application.persistentDataPath + "/userData.Json");
            userList = JsonMapper.ToObject<List<UserData>>(json);
            //存入字典
            for (int i = 0; i < userList.Count; i++)
            {
                userDic.Add(userList[i].username, userList[i]);
            }

        }
    }

    /// <summary>
    /// 更新或者存入UserData数据库
    /// </summary>
    /// <param name="userData"></param>
    public void AddUserData(UserData userData)
    {
        //如果存在用户就跟新
        if (userDic.ContainsKey(userData.username))
        {
            userDic[userData.username] = userData;
            
        }
        else
        {
            userDic.Add(userData.username, userData);
        }
        

    }
    /// <summary>
    /// 保存当前所有用户数据，存入Json
    /// </summary>
    public void SaveData()
    {
        List<UserData> userDatasList=new List<UserData>();
        //字典转换为List
        foreach (var item in userDic)
        {
            userDatasList.Add(item.Value);
        }
        string json = JsonMapper.ToJson(userDatasList);
        File.WriteAllText(Application.persistentDataPath + "/userData.Json", json, System.Text.Encoding.UTF8);
        Debug.Log(Application.persistentDataPath);
    }

    public string GetPasswordByName(string userName)
    {
        if (userDic.ContainsKey(userName))
        {
            return userDic[userName].password;
        }
        else
        {
            return null;
        }
    }

    public void SaveMyUser(UserData user)
    {

        myUser = userDic[user.username];
    }

    public UserData GetMyUser()
    {
        return myUser;
    }

    public string GetMyUserId()
    {
        return myUser.username;
    }
}
