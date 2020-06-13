using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class UIMenuLoader : MonoBehaviour
    {      
        void Start()
        {
            LoadHomeMenu();           
        }

        private void LoadHomeMenu()
        {
            if (SceneManager.GetSceneByName("UIMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("UIMenu", LoadSceneMode.Additive);
            }
            else
            {
                GameObject.Find("Canvas_UIMenu").GetComponent<UIManager>().UpdateUserInfo();
            }
        }
    }
}