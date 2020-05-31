using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        //public static UIManager instance;

        public GameObject startMenu;
        public InputField usernameField;

        /*Menu Btn*/
        #region Menu Btn
        public Button homeButton;
        public Button collectionButton;        
        public Button Btn_03;        
        #endregion

        public void ShowCollection()
        {
            //startMenu.SetActive(false);
            //collectionButton.interactable = false;           
            ClientSend.SwitchScene(Constants.SCENE_COLLECTION);

        }
        public void ShowHomePage()
        {
            ClientSend.SwitchScene(Constants.SCENE_HOMEPAGE);
        }
        public void ShowNewScene()
        {
            ClientSend.EnterDungeon("instance_01_name");
        }
    }
}
