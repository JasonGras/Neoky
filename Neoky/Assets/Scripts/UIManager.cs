using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        //public static UIManager instance;

        //public GameObject startMenu;
        //public InputField usernameField;

        public TMP_Text username_lbl;
        public TMP_Text text_level_lbl;
        public TMP_Text level_lbl;
        public Image _levelProgressBar;
        public TMP_Text text_golds_lbl;
        public TMP_Text text_diams_lbl;

        private void Start()
        {
            UpdateUserInfo();
        }

        public void ShowCollection()
        {  
            ClientSend.UpdatePlayerCollection();
        }
        public void ShowHomePage()
        {
            ClientSend.SwitchScene(Constants.SCENE_HOMEPAGE);
        }
        public void EnterDungeon()
        {
            ClientSend.EnterDungeon("instance_01_name");
        }

        public void SetProgressLevel()
        {
            _levelProgressBar.fillAmount = GameManager.players[Client.instance.myId].levelXp / GameManager.players[Client.instance.myId].requiredLvlUpXp; // Get % of progress type = 0,xx
        }
        public void UpdateUserInfo()
        {
            username_lbl.text = GameManager.players[Client.instance.myId].username;
            level_lbl.text = GameManager.players[Client.instance.myId].level.ToString();
            text_golds_lbl.text = GameManager.players[Client.instance.myId].golds.ToString();
            text_diams_lbl.text = GameManager.players[Client.instance.myId].diams.ToString();
            text_level_lbl.text = LocalizationSystem.GetLocalizedValue(Constants.text_level_lbl);
            SetProgressLevel();
        }
    }
}
