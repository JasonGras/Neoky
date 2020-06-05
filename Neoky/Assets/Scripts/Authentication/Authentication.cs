using UnityEngine;
using UnityEngine.UI;

// Required for all examples
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider.Model;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.PlayerLoop;
using TMPro;

namespace Assets.Scripts
{
    public class Authentication : MonoBehaviour
    {
        public static Authentication Auth;

        // AWS Inputs

        public TMP_Text errorMessage;
        public TMP_Text successMessage;
        //public Text signUpSent;
        public Image errorImageBG;

        // UNITY Inputs
        public TMP_InputField _username;
        public TMP_InputField _password;
        public Button _connexion;

        //public InputField _password;  
        private void Awake()
        {
            if (Auth == null)
            {
                Auth = this;
            }
            else if (Auth != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
            //myGUID = GUID.Generate(); 
            //Id = GetId();
        }
        private void Start()
        {
            if (SceneManager.GetSceneByName(Constants.SCENE_LOADING).isLoaded == true)
            {
                SceneManager.UnloadSceneAsync(Constants.SCENE_LOADING);
            }
        }

        public void AuthenticateUsers()
        {

            //ClientSend.TEST("TEST");
            Debug.Log("Authentication Required for " + _username.text);

            if (CheckUserPattern(_username.text))
            {
                if (CheckPasswordPattern(_password.text))
                {
                    _connexion.enabled = false;
                    if (errorImageBG.gameObject.activeSelf)
                    {
                        errorImageBG.gameObject.SetActive(false);
                    }
                    errorMessage.text = "";
                    if (!Client.instance.tcp.socket.Connected)
                    {
                        _connexion.enabled = true;
                        Debug.Log("Le serveur de jeu est inacessible pour le moment, veuillez réessayer ultèrieurement.");
                        //Client.instance.tcp.Connect(); // Connect tcp, udp gets connected once tcp is done
                    }
                    else
                    {
                        ClientSend.LogInToCognito(_username.text, _password.text);
                    }
                    
                }
            }
            //_ = AuthenticateFoncAsync(_username.text, _password.text);
        }
        public void GoToSignUpPage() // Called by SignUp Btn
        {
            if (SceneManager.GetSceneByName(Constants.SCENE_SIGNUP).isLoaded == false)
            {
                if (SceneManager.GetSceneByName(Constants.SCENE_AUTHENTICATION).isLoaded)
                {
                    SceneManager.UnloadSceneAsync(Constants.SCENE_AUTHENTICATION);
                }
                SceneManager.LoadSceneAsync(Constants.SCENE_SIGNUP, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync(Constants.SCENE_SIGNUP);
            }
        }

        public void GoToForgotPasswordPage() // Called by Forgot Pwd Link Btn
        {
            if (SceneManager.GetSceneByName(Constants.SCENE_FORGOT_PASSWORD_REQUEST).isLoaded == false)
            {
                if (SceneManager.GetSceneByName(Constants.SCENE_AUTHENTICATION).isLoaded)
                {
                    SceneManager.UnloadSceneAsync(Constants.SCENE_AUTHENTICATION);
                }
                SceneManager.LoadSceneAsync(Constants.SCENE_FORGOT_PASSWORD_REQUEST, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync(Constants.SCENE_FORGOT_PASSWORD_REQUEST);
            }
        }

        public bool CheckUserPattern(string _text)
        {
            string pattern;
            pattern = @"^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$"; // Username pattern

            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(_text))
            {
                return true;
            }
            else
            {
                errorImageBG.gameObject.SetActive(true);
                errorMessage.text = LocalizationSystem.GetLocalizedValue(Constants.error_username_format_lbl);
                //Debug.LogWarning("Le format de votre Nom d'utilisateur est incorrect.");
                return false;
            }
        }

        public bool CheckPasswordPattern(string _text)
        {
            string pattern;
            pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})"; // Password pattern (1 Min 1 Maj 1 Numeric 1 Symbol)

            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(_text))
            {
                return true;
            }
            else
            {
                errorImageBG.gameObject.SetActive(true);
                errorMessage.text = LocalizationSystem.GetLocalizedValue(Constants.error_password_format_lbl);
                //Debug.LogWarning("Le format du Mot de passe est incorrect.");
                return false;
            }
        }

        public void UpdateSceneMessage(string message)
        {
            _connexion.enabled = true;
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            errorMessage.text = message;
        }

        public void UpdateSceneSuccessMessage(string message)
        {
            _connexion.enabled = true;
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            successMessage.text = message;
        }

    }
}