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
            Debug.Log("Reconnexion To Server.");
            Client.instance.ConnectToServer();
            GameManager.instance.SwitchToScene(Constants.SCENE_LOADING, Constants.SCENE_MAINTENANCE);
            //SceneManager.LoadSceneAsync(Constants.SCENE_LOADING, LoadSceneMode.Additive);
        }
    }
}