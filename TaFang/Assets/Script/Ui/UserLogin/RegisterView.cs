using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterView : ViewBasePanel
{

	public UserData GetUser()
    {
        if (GetControl<Text>("PasswordInText1").text != GetControl<Text>("PasswordInText2").text)
        {
            Debug.Log("两次输入的密码不一样,请重新输入");
            GetControl<Text>("PasswordInText1").text = "";
            GetControl<Text>("PasswordInText2").text = "";
            return null;
        }
        else
        {
            UserData user = new UserData(GetControl<Text>("AccountinText").text, GetControl<Text>("PasswordInText1").text);
            return user;
        }
    }

    
}
