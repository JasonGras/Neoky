using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class InitClient : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Login To Server.");
            Client.instance.ConnectToServer();
            SceneManager.LoadSceneAsync(Constants.SCENE_LOADING, LoadSceneMode.Additive);
        }
    }
}
