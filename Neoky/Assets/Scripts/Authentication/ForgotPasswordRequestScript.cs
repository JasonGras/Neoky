using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ForgotPasswordRequestScript : MonoBehaviour
    {
        public static ForgotPasswordRequestScript ForgotPwdRequest;

        // UNITY Inputs
        public TMP_Text errorMessage;
        public TMP_Text successMessage;

        public Image errorImageBG;

        public TMP_InputField username;
        public TMP_InputField email;

        public Button RedefinePwdBtn;

        private void Awake()
        {
            if (ForgotPwdRequest == null)
            {
                ForgotPwdRequest = this;
            }
            else if (ForgotPwdRequest != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        public void SubmitForgotPasswordRequest()
        {
            if (CheckUserPattern(username.text))
            {
                if (CheckEmailPattern(email.text))  
                {
                    RedefinePwdBtn.enabled = false;
                    if (errorImageBG.gameObject.activeSelf)
                    {
                        errorImageBG.gameObject.SetActive(false);
                    }
                    errorMessage.text = "";
                    Debug.Log("ForgotPasswordRequest sent with my Data : " + username.text + " | " + email.text);

                    // Ask server to redefine PWD
                    ClientSend.ForgotPasswordRequest(username.text, email.text);
                    GameManager.instance.SwitchToScene(Constants.SCENE_FORGOT_PASSWORD, Constants.SCENE_FORGOT_PASSWORD_REQUEST);
                }


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
        public void UpdateErrorSceneMessage(string message)
        {
            RedefinePwdBtn.enabled = true;
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            errorMessage.text = message;
        }
        public void UpdateSuccessSceneMessage(string message)
        {
            RedefinePwdBtn.enabled = true;
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            successMessage.text = message;
        }

        public void GoToAuthenticationPage() // Return our Canceel Btn to go to previous page
        {
            GameManager.instance.SwitchToScene(Constants.SCENE_AUTHENTICATION, Constants.SCENE_FORGOT_PASSWORD);
        }
    }
}




