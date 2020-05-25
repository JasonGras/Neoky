using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RedefinePasswordScript : MonoBehaviour
    {

        public static RedefinePasswordScript RedefinePwd;

        // UNITY Inputs
        public TMP_Text errorMessage;

        public Image errorImageBG;

        public TMP_InputField username;
        public TMP_InputField currentPassword;
        public TMP_InputField newPassword;
        public TMP_InputField confirmedNewPassword;

        public Button RedefinePwdBtn;

        private void Awake()
        {
            if (RedefinePwd == null)
            {
                RedefinePwd = this;
            }
            else if (RedefinePwd != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
            //myGUID = GUID.Generate(); 
            //Id = GetId();
        }

        public void SubmitRedefinedPassword()
        {
            if (CheckUserPattern(username.text))
            {
                if (CheckPasswordPattern(currentPassword.text))
                {
                    if (CheckPasswordPattern(newPassword.text))
                    {
                        if (CheckConfirmedNewPasswordPattern(confirmedNewPassword.text))
                        {

                            RedefinePwdBtn.enabled = false;
                            if (errorImageBG.gameObject.activeSelf)
                            {
                                errorImageBG.gameObject.SetActive(false);
                            }
                            errorMessage.text = "";
                            Debug.Log("RedefinedUserPassword sent with my Data : "+ username.text+ " | "+currentPassword.text + " | " + newPassword.text);
                            ClientSend.RedefinedUserPassword(username.text, currentPassword.text, newPassword.text);//.SwitchScene(Constants.SCENE_HOMEPAGE);

                        }
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
        public void UpdateSceneMessage(string message)
        {
            RedefinePwdBtn.enabled = true;
            if (!errorImageBG.gameObject.activeSelf)
            {
                errorImageBG.gameObject.SetActive(true);
            }
            errorMessage.text = message;
        }

    }
}
