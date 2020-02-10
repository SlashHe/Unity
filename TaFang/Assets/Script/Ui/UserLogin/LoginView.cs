using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : ViewBasePanel {

	public string GetUserName()
    {
        return GetControl<Text>("AccountinText").text;
    }

    public string GetPassword()
    {
        return GetControl<InputField>("PasswordInputField").text;
    }
	
    public void SetUserName(string userName)
    {
        GetControl<Text>("AccountinText").text = userName;
    }

    public void SetPassword(string password)
    {
        GetControl<InputField>("PasswordInputField").text = password;
    }
}
