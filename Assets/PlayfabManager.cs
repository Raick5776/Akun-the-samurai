using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayfabManager : MonoBehaviour
{
    public Text messageText;
    public TMP_InputField emailinput;
    public TMP_InputField passwordinput;

    public TMP_InputField emailinput1;

    public TMP_InputField emailinput2;
    public TMP_InputField passwordinput1;


    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailinput.text,
            Password = passwordinput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registrato!";
        Debug.Log("Registrato");
    }




    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailinput1.text,
            Password = passwordinput1.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    public void ResetPasswordButton()
    {

        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailinput2.text,
            TitleId = "E678E"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);

    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password Reset";
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);

    }

    void OnSucces(LoginResult result)
    {
        Debug.Log("Succesfull login/Account Create!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while lohin in");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLoginSucces(LoginResult result)
    {
        Debug.Log("Login avvenuto con successo");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    /*
    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest { 
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate { 
                    StatisticName = "PlatformScore" ,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request , OnleaderboardUpdate , OnError);
    }
    void OnleaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successo LeaderBoard");
    } */
}