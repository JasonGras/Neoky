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

        public InputField username;
        public InputField password;
        public InputField email;
        public Button signUpBtn;

        // Au chargement de la page, je me connecte au serveur
        private void Start()
        {
            Client.instance.ConnectToServer();
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
                        ClientSend.SignUpToCognito(username.text, password.text, email.text);//.SwitchScene(Constants.SCENE_HOMEPAGE);
                        //_ = SignUpUser(username.text, password.text, email.text, CLIENTAPP_ID);

                    }
                }
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
                errorMessage.text = "Le format de l'adresse email est incorrect.";
                Debug.LogWarning("Le format de l'adresse email est incorrecte.");
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
                errorMessage.text = "Le format de votre Nom d'utilisateur est incorrect.";
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
                errorMessage.text = "Les mots de passe doivent contenir au moins 8 caractères et contenir au moins : majuscules, minuscules, chiffres et symboles.";
                Debug.LogWarning("Le format du Mot de passe est incorrect.");
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
            signUpSent.text = "Un mail de confirmation de creation de compte vous a été envoyé. \nMerci de cliquer sur le lien dans le mail pour finaliser votre inscription.";
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
