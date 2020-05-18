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
           // if (Client.instance.tcp.socket.Connected)
           //{
           StartCoroutine(LoadAsyncOperation());

            /*}
            else
            {
                Debug.LogError("Serveur de Jeu indisponible pour le moment.");
                // [AF] Affichage Serveur Indisponible (Image ?)
                // [AF] Show Bouton Retry Connexion
            }*/
        }
        
        IEnumerator LoadAsyncOperation()
        {
            AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Authentication", LoadSceneMode.Additive);
            

            while (!loadingScene.isDone)
            {

                _progressBar.fillAmount = loadingScene.progress;
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
 