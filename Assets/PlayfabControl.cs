using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class PlayfabControl : MonoBehaviour
{
    [SerializeField] GameObject singupTab, logInTab, HUD, Livello;

    public TextMeshProUGUI LoginEmail, LoginPassword, RegisterEmail, RegisterPassword, errorLogin, errorSignUp;

    string encryptedPassword;


    public void SingUpTab()
    {
        singupTab.SetActive(true);
        logInTab.SetActive(false);
        HUD.SetActive(false);
        errorSignUp.text = "";
        errorLogin.text = "";
    }

    public void Login()
    {
        singupTab.SetActive(false);
        logInTab.SetActive(true);
        HUD.SetActive(false);
        errorSignUp.text = "";
        errorLogin.text = "";
    }


    public void Registrazione()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = RegisterEmail.text, Password = RegisterPassword.text };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSucces, RegisterError);
    }

    public void RegisterSucces(RegisterPlayFabUserResult result)
    {
        errorSignUp.text = "";
        errorSignUp.text = "";
        errorLogin.text = "";
        StartGame();
    }

    public void RegisterError(PlayFabError error)
    {
        errorSignUp.text = error.GenerateErrorReport();
    }

    public void LoginError(PlayFabError error)
    {
        errorLogin.text = error.GenerateErrorReport();
    }

    public void LoginSucces(LoginResult result)
    {
        StartGame();
    }

    public void LogIn()
    {
        var request = new LoginWithEmailAddressRequest { Email = LoginEmail.text, Password = LoginPassword.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSucces, LoginError);
    }
    void StartGame()
    {
        HUD.SetActive(false);
        Livello.SetActive(true);
    }
}