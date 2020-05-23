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
using Amazon.Runtime.Internal;

using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

// Required for the GetS3BucketsAsync example
//using Amazon.S3;
//using Amazon.S3.Model;

namespace Assets.Scripts
{
    public class SignUpScript : MonoBehaviour
    {
        //private AmazonCognitoIdentityProviderClient _client;
        // UNITY Inputs
        public Text errorMessage;
        public Text signUpSent;
        public Image errorImageBG;

        public TMP_InputField username;
        public TMP_InputField password;
        public TMP_InputField email;
        public Button signUpBtn;

        // Au chargement de la page, je me connecte au serveur
        private void Start()
        {
            //Client.instance.ConnectToServer();
        }

        public void SignUpToCognito()
        {            
            if (CheckUserPattern(username.text))
            {
                if (CheckPasswordPattern(password.text))
                {
                    if (CheckEmailPattern(email.text))
                    {
                        signUpBtn.enabled = false;
                        if (errorImageBG.gameObject.activeSelf)
                        {
                            errorImageBG.gameObject.SetActive(false);
                        }
                        errorMessage.text = "";
                        string response = ClientSend.SignUpToCognito(username.text, password.text, email.text);//.SwitchScene(Constants.SCENE_HOMEPAGE);

                        switch (response)
                        {
                            case "SIGN_UP_SEND_OK" :
                                SignUpFinalised();
                                break;
                            case "SIGN_UP_SEND_KO":
                                SignUpShowError(Constants.signup_failed_lbl);
                                break;
                            default:
                                SignUpShowError(Constants.technical_error_lbl);
                                break;
                        }
                    }
                }
            }

        }

        public void GoToPreviousPage() // Return our Canceel Btn to go to previous page
        {

            GameManager.instance.SwitchToScene(Constants.SCENE_AUTHENTICATION, Constants.SCENE_SIGNUP);

            /*if (SceneManager.GetSceneByName("Authentication").isLoaded == false)
            {
                if (SceneManager.GetSceneByName("SignUp").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("SignUp");
                }
                SceneManager.LoadSceneAsync("Authentication", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("Authentication");
            }*/
        }

        public bool CheckEmailPattern(string _text)
        {
            string pattern;
            pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"; // Email pattern

            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(_text))
            {
                return true;
            }
            else
            {
                errorImageBG.gameObject.SetActive(true);
                errorMessage.text = LocalizationSystem.GetLocalizedValue(Constants.error_email_format_lbl); 
                //Debug.LogWarning("Le format de l'adresse email est incorrecte.");
                return false;
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
                errorMessage.text = LocalizationSystem.GetLocalizedValue(Constants.error_username_format_lbl); ;
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

        //Pas encore trouvé pour l'appeler cette Fonction.
        public void SignUpFinalised()
        {
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            signUpSent.text = LocalizationSystem.GetLocalizedValue(Constants.signup_validation_email_lbl);
        }
        public void SignUpShowError(string _message)
        {
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            errorMessage.text = LocalizationSystem.GetLocalizedValue(_message);
        }


    }
}


/*
         catch (InvalidParameterException e)
        {           
            Debug.LogError("InvalidParameterException | Code : " + e.StatusCode.ToString() + " | Exeption : " + e.Message); 
            return;
        }
        catch (InvalidPasswordException e)
        {
            Debug.LogError("InvalidPasswordException | Code : " + e.StatusCode.ToString() + " | Exeption : " + e.Message);
            return;
        }
*/
