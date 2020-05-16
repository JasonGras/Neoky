using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LoadFirstScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Client.instance.ConnectToServer();

            if (SceneManager.GetSceneByName("Authentication").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("Authentication", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("Authentication");
            }
        }
        
    }
}