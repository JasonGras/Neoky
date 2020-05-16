using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class UIMenuLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            if (SceneManager.GetSceneByName("UIMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("UIMenu", LoadSceneMode.Additive);
            }
            else
            {
                //SceneManager.UnloadSceneAsync("UIMenu"); // Must Stay loaded = Will be Unloaded on PlayingScenes
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