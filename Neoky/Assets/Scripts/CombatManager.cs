using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CombatManager : MonoBehaviour
    {

        private void Start()
        {
            //Send the server We are Ready on Our Scene
            ClientSend.FightPackets("INIT_FIGHT");
            // => Init your player Crew and Enemy Crew
            // => Fight CountDown
            // => Fight Begin ! 

            // Exchange de Coups ! 
            // Atq / Def / Brise G

            // HP = 0 pour un parti
            // Fight Over
            // Reward the Winner / Back Home


        }
    }
}