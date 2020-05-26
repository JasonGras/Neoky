using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoadFirstScene : MonoBehaviour
    {
        [SerializeField]
        private Image _progressBar;


        // Start is called before the first frame update
        void Start()
        {
            if (Client.instance.tcp.socket.Connected)
            {
                StartCoroutine(LoadAsyncOperation());
            }
            else
            {

                Debug.Log("LoadFirstScene.cs | Server injoignable.");
                Debug.Log("LoadFirstScene.cs | Socket.Connected :" + Client.instance.tcp.socket.Connected+" | Client.instance.myId :"+ Client.instance.myId);

                GameManager.instance.SwitchToScene(Constants.SCENE_MAINTENANCE, Constants.SCENE_LOADING);
                //SceneManager.LoadSceneAsync(Constants.SCENE_MAINTENANCE, LoadSceneMode.Additive);
                //Debug.Log("LoadFirstScene.cs | Serveur de Jeu indisponible pour le moment.");
                // [AF] Affichage Serveur Indisponible (Image ?)
                // [AF] Show Bouton Retry Connexion
            }
        }
        
        IEnumerator LoadAsyncOperation()
        {
            AsyncOperation loadingScene = SceneManager.LoadSceneAsync(Constants.SCENE_AUTHENTICATION, LoadSceneMode.Additive);
            

            while (!loadingScene.isDone)
            {
                _progressBar.fillAmount = loadingScene.progress;
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
 