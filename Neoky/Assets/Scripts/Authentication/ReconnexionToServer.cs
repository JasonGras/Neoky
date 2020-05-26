using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ReconnexionToServer : MonoBehaviour
    {
        public void ReconnexionButton()
        {
            Debug.Log("Reconnexion To Server Lunch.");
            Client.instance.ConnectToServer();
            Debug.Log("Reconnexion To Server Done.");
            GameManager.instance.SwitchToScene(Constants.SCENE_LOADING, Constants.SCENE_MAINTENANCE);
            Debug.Log("Go To LoadingScene Done.");
            //SceneManager.LoadSceneAsync(Constants.SCENE_LOADING, LoadSceneMode.Additive);
        }
    }
}