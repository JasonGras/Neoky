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
        public TMP_Text text_coin_lbl;

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

        public void ShowLootPage()
        {
            ClientSend.SwitchScene(Constants.SCENE_LOOTS);
        }

        public void EnterDungeon()
        {
            ClientSend.EnterDungeon("instance_01_name");
        }

        public void OpenCoin()
        {
            ClientSend.OpenCoin(CoinLoots.Coin.Viking,CoinLoots.CoinAverageQuality.Green);
            ClientSend.OpenCoin(CoinLoots.Coin.Viking,CoinLoots.CoinAverageQuality.Blue);
            ClientSend.OpenCoin(CoinLoots.Coin.Viking,CoinLoots.CoinAverageQuality.Purple);
            ClientSend.OpenCoin(CoinLoots.Coin.Viking,CoinLoots.CoinAverageQuality.Gold);
        }

        public void SetProgressLevel()
        {
            _levelProgressBar.fillAmount = GameManager.players[Client.instance.myId].levelXp / GameManager.players[Client.instance.myId].requiredLvlUpXp; // Get % of progress type = 0,xx
        }
        public void UpdateUserInfo()
        {
            username_lbl.text = GameManager.players[Client.instance.myId].username;
            level_lbl.text = GameManager.players[Client.instance.myId].level.ToString();

            int NbCoins = 0;
            foreach (var CoinType in GameManager.players[Client.instance.myId].coin)
            {
                NbCoins += CoinType.Value;
            }

            text_coin_lbl.text = NbCoins.ToString();
            text_golds_lbl.text = GameManager.players[Client.instance.myId].golds.ToString();
            text_diams_lbl.text = GameManager.players[Client.instance.myId].diams.ToString();
            text_level_lbl.text = LocalizationSystem.GetLocalizedValue(Constants.text_level_lbl);
            SetProgressLevel();
        }
    }
}
