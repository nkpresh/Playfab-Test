using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TopText;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLogin;
    [SerializeField] TMP_InputField PasswordLogin;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField UsernameRegister;
    [SerializeField] TMP_InputField EmailRegister;
    [SerializeField] TMP_InputField PasswordRegister;
    [SerializeField] GameObject RegisterPage;

    [Header("Recovery")]
    [SerializeField] TMP_InputField EmailRecovery;
    [SerializeField] GameObject RecoverPage;

    [SerializeField] GameObject WelcomePopup;
    [SerializeField] TextMeshProUGUI welcomeText;

    void Start()
    {
        
    }

    #region Button functions

    public void RegisterUser()
    {


        var request = new RegisterPlayFabUserRequest
        {
            DisplayName=UsernameRegister.text, 
            Password=PasswordRegister.text, 
            Email=EmailRegister.text,
            RequireBothUsernameAndEmail=false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSuccess,OnError);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailLogin.text,
            Password = PasswordLogin.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSuccess,OnError);
    }

    public void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest {
            Email = EmailRecovery.text,
            TitleId="61F93"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnrecoverySuccess, OnError);
    }

    private void OnrecoverySuccess(SendAccountRecoveryEmailResult obj)
    {
        OpenLoginPage();
        MessageText.text = "Recovery mail sent";
    }

    private void OnLoginSuccess(LoginResult result)
    {
        string name = result.InfoResultPayload.PlayerProfile.DisplayName;
        WelcomePopup.SetActive(true);
        welcomeText.text = "Welcome "+name;
        MessageText.text = "Login Success";

        GameManager.instance.playerName=name;
        StartCoroutine(LoadNext());
    }

    private void OnError(PlayFabError error)
    {
        MessageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnregisterSuccess(RegisterPlayFabUserResult result)
    {
        MessageText.text = "New Account is created";
        OpenLoginPage();
    }

    public void OpenLoginPage()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoverPage.SetActive(false);
        TopText.text = "Login";
    }

    public void OpenRegisterPage()
    { 
        RegisterPage.SetActive(true);
        LoginPage.SetActive(false);
        RecoverPage.SetActive(false);
        TopText.text = "Register";

    }
    public void OpenRecoveryPage()
    {
        RecoverPage.SetActive(true);
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        TopText.text = "Recovery";
    }

    #endregion

    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Thegame");
    }
}
