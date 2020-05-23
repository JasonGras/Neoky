using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIMenuLoader : MonoBehaviour
    {
        public TMP_Text username_lbl;
        [SerializeField]
        public TMP_Text text_level_lbl;
        [SerializeField]
        public TMP_Text level_lbl;
        [SerializeField]
        public Image _levelProgressBar;

        void Start()
        {
            LoadHomeMenu();
            username_lbl.text = GameManager.players[Client.instance.myId].username;
            level_lbl.text = GameManager.players[Client.instance.myId].level.ToString();
            text_level_lbl.text = LocalizationSystem.GetLocalizedValue(Constants.text_level_lbl);
            SetProgressLevel();
        }

        private void LoadHomeMenu()
        {
            if (SceneManager.GetSceneByName("UIMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("UIMenu", LoadSceneMode.Additive);
            }
        }
        public void SetProgressLevel()
        {
            _levelProgressBar.fillAmount = GameManager.players[Client.instance.myId].levelXp/GameManager.players[Client.instance.myId].requiredLvlUpXp; // Get % of progress type = 0,xx
        }
    }
}