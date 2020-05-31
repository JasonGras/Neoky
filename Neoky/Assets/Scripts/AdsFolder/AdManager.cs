using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{    
    string gameId = "3611075";
    bool testMode = true;

    void Start()
    {
        //Advertisement.Initialize(gameId, testMode);
    }

    public void GetFreeReward()
    {
        /*if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video"); //or empty
        }
        */
    }

    public void isStillAuthenticated()
    {
        ClientSend.isStillAuthenticated();
    }
}
