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

public class SignUpScript : MonoBehaviour
{
    // AWS Inputs
    private string POOL_ID = "eu-west-2_htPUpskO5";
    private string CLIENTAPP_ID = "5e3ri4nan0b2vrovmtcbmm18bo";
    private string FED_POOL_ID = "eu-west-2:1685b7b4-a171-492b-ad7c-62527ccd80d1";
    private static RegionEndpoint REGION = RegionEndpoint.EUWest2;

    //private AmazonCognitoIdentityProviderClient _client;
    // UNITY Inputs
    public Text errorMessage;
    public Text signUpSent;
    public Image errorImageBG;

    public InputField username;
    public InputField password;  
    public InputField email;  
    public Button signUpBtn;  


    public void SignUpToCognito()
    {
        if (CheckUserPattern(username.text))
        {
            if (CheckPasswordPattern(password.text))
            {
                if (CheckEmailPattern(email.text))
                {
                    signUpBtn.enabled = false;
                    if (!errorImageBG.gameObject.activeSelf)
                    {
                        errorImageBG.gameObject.SetActive(true);
                    }
                    errorMessage.text = "";
                    signUpSent.text = "Un mail de confirmation de creation de compte vous a été envoyé. \nMerci de cliquer sur le lien de vérification.";
                    _ = SignUpUser(username.text, password.text, email.text, CLIENTAPP_ID);

                }
            }
        }

    }

    private static async Task SignUpUser(string _username, string _password, string _email, string _clientAppId)
    {
        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), REGION);

        SignUpRequest signUpRequest = new SignUpRequest()
        {
            ClientId = _clientAppId,
            Username = _username,
            Password = _password
        };

        List<AttributeType> attributes = new List<AttributeType>()
        {
            new AttributeType(){Name="email", Value = _email},
            new AttributeType(){Name="custom:Money", Value = "1000"}
        };

        // Send SignupRequest
        signUpRequest.UserAttributes = attributes;

        Debug.Log("Init.");
        try
        {
            SignUpResponse result = await provider.SignUpAsync(signUpRequest);

            if(result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.Log("Creation de Compte Finalisée.");                
            }
        }
        catch (Exception e)
        {
            Debug.LogError("New Exception | Code : " + e.GetType().ToString() + " | Exeption : " + e.Message);
            return;
        }
        Debug.Log("Over.");

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
