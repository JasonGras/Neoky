using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{

    public enum BattleState { INIT_FIGHT, PLAYER_TURN, ENEMY_TURN, WON, LOST }
    

    public class CombatManager : MonoBehaviour
    {
        public BattleState state;
        public float distFromCam = 10; //distance of the emitter from the camera

        private void Start()
        {

            UnloadMenuScene();
            DisableClickParticleSystem();
            //Send the server We are Ready on Our Scene
            state = BattleState.INIT_FIGHT;
            ClientSend.FightPackets("INIT_FIGHT");
            // => Init your player Crew and Enemy Crew
            // => Fight CountDown
            // => Fight Begin ! 

            // Exchange de Coups ! 
                // => Client Send AttackToServer
                // 
            // Atq / Def / Brise G

            // HP = 0 pour un parti
            // Fight Over
            // Reward the Winner / Back Home


        }

        /*private void Update()
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distFromCam));
                    if (touch.phase == TouchPhase.Began)
                    {

                        Debug.Log("TouchBegan : " + pos.x + " | " + pos.y + " | " + pos.z);
                        //emitter.transform.position = pos;
                        //emitter.Play();
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Debug.Log("TouchEnded : " + pos.x + " | " + pos.y + " | " + pos.z);
                        //recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        Debug.Log("TouchStationary : " + pos.x + " | " + pos.y + " | " + pos.z);
                        //recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled) // Ipad on Face or 6 figers
                    {
                        Debug.Log("TouchCanceled : " + pos.x + " | " + pos.y + " | " + pos.z);
                        //recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }*/

        public void OnAttackButton_Member_01()
        {

        }

        private void UnloadMenuScene()
        {
            if (SceneManager.GetSceneByName("UIMenu").isLoaded == true)
            {
                SceneManager.UnloadSceneAsync("UIMenu", UnloadSceneOptions.None);
            }
        }

        private void DisableClickParticleSystem()
        {
            // Get GameObjectByname : PS_Click
            //GameObject Go = GameObject.Find("PS_Click");
            //Go.SetActive(false);
            // Disable GameObject
        }


        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void AttackPlayerMemberCrew(NeokyCollection _playerUnit,NeokyCollection _enemyUnit)
        {
            Transform PlayerUnitTransform = GameObject.Find(_playerUnit.collection_prefab).transform;
        }
    }
}