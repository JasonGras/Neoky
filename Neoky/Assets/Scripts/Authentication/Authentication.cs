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

namespace Assets.Scripts
{
    public class Authentication : MonoBehaviour
    {
        public static Authentication Auth;

        // AWS Inputs

        public Text errorMessage;
        //public Text signUpSent;
        public Image errorImageBG;

        // UNITY Inputs
        public InputField _username;
        public InputField _password;
        public Button _connexion;
        //public InputField _password;  

        public void AuthenticateUsers()
        {
            //Debug.Log("Authentication Required for " + _username.text);
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
                    ClientSend.LogInToCognito(_username.text, _password.text);
                }
            }
            //_ = AuthenticateFoncAsync(_username.text, _password.text);
        }
        public void GoToSignUpPage()
        {
            if (SceneManager.GetSceneByName("SignUp").isLoaded == false)
            {
                if (SceneManager.GetSceneByName("Authentication").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("Authentication");
                }
                SceneManager.LoadSceneAsync("SignUp", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("SignUp");
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
                errorMessage.text = "Le nom de compte est incorrect.";
                Debug.LogWarning("Le format de votre Nom d'utilisateur est incorrect.");
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
                errorMessage.text = "Votre mot de passe est incorrect.";
                Debug.LogWarning("Le format du Mot de passe est incorrect.");
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
        
    }
}