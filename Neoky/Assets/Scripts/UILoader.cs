using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class UILoader : MonoBehaviour
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

            /*
            if(SceneManager.GetSceneByName("UIMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("UIMenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("UIMenu");
            }   */
        }
    }
}