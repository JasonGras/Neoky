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


public class Authentication : MonoBehaviour
{
    // AWS Inputs
    private string POOL_ID = "eu-west-2_htPUpskO5";
    private string CLIENTAPP_ID = "5e3ri4nan0b2vrovmtcbmm18bo";
    private string FED_POOL_ID = "eu-west-2:1685b7b4-a171-492b-ad7c-62527ccd80d1";
    private RegionEndpoint REGION = RegionEndpoint.EUWest2;

    private AmazonCognitoIdentityProviderClient _client;
    // UNITY Inputs
    public InputField _username;
    public InputField _password;
    //public InputField _password;  

    public void AuthenticateUsers()
    {
        _ = AuthenticateFoncAsync(_username.text, _password.text);
    }

    public async Task AuthenticateFoncAsync(string _username, string _password)
    {
        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), REGION);

        CognitoUserPool userPool = new CognitoUserPool(POOL_ID, CLIENTAPP_ID, provider);

        CognitoUser user = new CognitoUser(_username, CLIENTAPP_ID, userPool, provider);

        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = _password
        };

        AuthFlowResponse authFlowResponse = null;
        try
        {
            authFlowResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            Debug.Log("Login Lunch");
        }
        catch (Exception e)
        {
            Debug.LogError("Login Failed : " + e);
            return;
        }

        Debug.Log("Login Seems Ok");

        // Get users Attribute
        GetUserRequest getUserRequest = new GetUserRequest();
        getUserRequest.AccessToken = authFlowResponse.AuthenticationResult.AccessToken;
        Debug.Log("GetUserAttribute Seems Ok");

        //Get User Values
        GetUserResponse getUser = await provider.GetUserAsync(getUserRequest);
        string _curMoney = getUser.UserAttributes.Where(a => a.Name == "custom:Money").First().Value;
        int _userMoney = Convert.ToInt32(_curMoney);

        Debug.Log("Total sur le compte de" + _username + ":" + _userMoney);

        // Attribute type definition
        AttributeType attributeType = new AttributeType()
        {
            Name = "custom:Money",
            Value = Convert.ToString(_userMoney + 10),// Valeur mise a jour
        };


        // Update Attribute Request
        UpdateUserAttributesRequest updateUserAttributesRequest = new UpdateUserAttributesRequest()
        {
            AccessToken = authFlowResponse.AuthenticationResult.AccessToken
        };

        updateUserAttributesRequest.UserAttributes.Add(attributeType);
        provider.UpdateUserAttributes(updateUserAttributesRequest);

        Debug.Log("+10 on the Money of account " + _username);
    }
}
