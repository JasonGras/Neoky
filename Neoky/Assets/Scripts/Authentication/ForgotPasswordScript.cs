using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ForgotPasswordScript : MonoBehaviour
    {
        public static ForgotPasswordScript ForgotPwd;

        // UNITY Inputs
        public TMP_Text errorMessage;
        public TMP_Text successMessage;

        public Image errorImageBG;

        public TMP_InputField username;
        public TMP_InputField newPassword;
        public TMP_InputField confirmationNewPassword;
        public TMP_InputField code;

        public Button RedefinePwdBtn;

        private void Awake()
        {
            if (ForgotPwd == null)
            {
                ForgotPwd = this;
            }
            else if (ForgotPwd != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
            //myGUID = GUID.Generate(); 
            //Id = GetId();
        }

        public void SubmitForgotPassword()
        {
            if (CheckUserPattern(username.text))
            {
                if (CheckPasswordPattern(newPassword.text)) // Code Check ? 
                {
                    if (CheckConfirmedNewPasswordPattern(confirmationNewPassword.text)) // Code Check ? 
                    {
                        //if (CheckPasswordPattern(currentPassword.text)) // Code Check ? 
                        //{
                        RedefinePwdBtn.enabled = false;
                        if (errorImageBG.gameObject.activeSelf)
                        {
                            errorImageBG.gameObject.SetActive(false);
                        }
                        errorMessage.text = "";
                        Debug.Log("ForgotUserPassword sent with my Data : " + username.text + " | " + code.text + " | " + newPassword.text);

                        // Ask server to redefine PWD
                        ClientSend.ForgotUserPassword(username.text, code.text, newPassword.text);
                        // }
                    }
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
        public bool CheckConfirmedNewPasswordPattern(string _text)
        {
            if (_text == newPassword.text)
            {
                return true;
            }
            else
            {
                errorImageBG.gameObject.SetActive(true);
                errorMessage.text = LocalizationSystem.GetLocalizedValue(Constants.error_confirmed_password_format_lbl);
                //Debug.LogWarning("Le format du Mot de passe est incorrect.");
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
