using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("SignUp").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("SignUp", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("SignUp");
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
