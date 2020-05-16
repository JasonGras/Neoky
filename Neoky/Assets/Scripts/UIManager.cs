using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public GameObject startMenu;
        public InputField usernameField;

        /*Menu Btn*/
        #region Menu Btn
        public Button homeButton;
        public Button collectionButton;        
        #endregion


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        /// <summary>Attempts to connect to the server.</summary>
        //public void ConnectToServer()
        //{
        //    startMenu.SetActive(false);
        //    usernameField.interactable = false;
        //    Client.instance.ConnectToServer();
        //}
        public void ShowCollection()
        {
            //startMenu.SetActive(false);
            //collectionButton.interactable = false;           
            ClientSend.SwitchScene(Constants.SCENE_COLLECTION);

        }
        public void ShowHomePage()
        {
            //startMenu.SetActive(false);
            //homeButton.interactable = false;
            ClientSend.SwitchScene(Constants.SCENE_HOMEPAGE);

        }
    }
}
