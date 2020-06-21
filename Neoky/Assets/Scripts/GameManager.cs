using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        //
        public static Dictionary<int, GameObject> PlayerCrew = new Dictionary<int, GameObject>();
        public static Dictionary<int, GameObject> EnemyCrew = new Dictionary<int, GameObject>();
        public static Dictionary<NeokyCollection, Dictionary<string,int>> AllPlayerUnits = new Dictionary<NeokyCollection, Dictionary<string, int>>();
        //public static List<NeokyCollection> AllGameUnits = new List<NeokyCollection>();

        public GameObject netPlayerPrefab;

        //[SerializeField]
        //public List<GameObject> PlayerCollectionList;

        public bool isSpawned_playerCrew { get; set; } = false;
        public bool isSpawned_EnemyCrew { get; set; } = false;



        //public Dictionary<string, GameObject> PlayerColle*ctionList = new Dictionary<string, GameObject>();


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

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

        /// <summary>Spawns a player.</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The player's name.</param>
        public void SpawnPlayer(int _id, string _username, float _level, float _levelxp, float _requiredLvlUpXp, string _startScene, string _unloadScene, float _golds, Dictionary<string,int> _coin, float _diams)
        {
            // Create Player Prefab
            GameObject _player = Instantiate(netPlayerPrefab);       
            
            // Initialize Player
            _player.GetComponent<PlayerManager>().Initialize(_id, _username, _level, _levelxp, _requiredLvlUpXp, _startScene, _golds, _coin, _diams);

            // Add player to a List of Players
            players.Add(_id, _player.GetComponent<PlayerManager>());

            // Send player to his Loged In Homepage
            SwitchToScene(_startScene, _unloadScene);
        }


        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void SpawnAllPlayerMemberCrew(Dictionary<int, NeokyCollection> _playerAllCrew)
        {
            isSpawned_playerCrew = false;
            foreach (var _crewUnit in _playerAllCrew)
            {
                // On PlayeCollectionUnit find the MemberCrewPrefab using _memberCrewPrefab Name
                Debug.Log("Find Prefab in PlayerCollectionList");
                
                //GameObject unitCrewPrefab = PlayerCollectionList.Where<GameObject>(x => x.name == _crewUnit.Value.collection_prefab).SingleOrDefault();

                if (_crewUnit.Value.local_Collection_prefab != null)
                {
                    Transform Spawn = GameObject.Find("Spawn_Player_" + _crewUnit.Key.ToString()).transform;
                    // Create Player Prefab
                    Debug.Log("Instantiate Crew Prefab");
                    GameObject CrewMember = Instantiate(_crewUnit.Value.local_Collection_prefab, Spawn);
                    CrewMember.transform.parent = Spawn; // Set the Member Spawn on the Spawn_X position
                                                         //CrewMember.transform.localPosition = new Vector3(0, 0, 0);
                                                         // Initialize Player
                    CrewMember.GetComponent<CrewMembers>().Initialize(_crewUnit.Key, _crewUnit.Value.collection_name, _crewUnit.Value.lifePoints);

                    // Add player to a List of Players
                    Debug.Log("CrewMember added to PlayerCrew");
                    PlayerCrew.Add(_crewUnit.Key, CrewMember);
                }
                else
                {
                    Debug.Log("You forgot to Add the Prefab of the Unit on the GameManager Size on the UnloadScene");
                }


                //Add Unit Image Btn

                // Get the parent Location
                Transform THorizontalBtnLayer = GameObject.Find("HorizontalBtnLayer").transform;
                //GameObject UnitImageBtn = GameObject.Find("UnitImageBtn");
                
                // Instantiate the Btn Image
                GameObject CrewMemberImgBtn = Instantiate(_crewUnit.Value.local_Collection_IMGBtn_prefab, THorizontalBtnLayer);
                // Set The parent
                //CrewMemberImgBtn.transform.parent = THorizontalBtnLayer;

                // Set the Image of the Button
                Image CrewUnitImageBtn =  CrewMemberImgBtn.GetComponent<Image>();
                CrewUnitImageBtn.sprite = _crewUnit.Value.local_Collection_image;

                CrewMemberImgBtn.GetComponentInChildren<Button>().onClick.AddListener(() => OnUseUnitBtn(_crewUnit.Key));

            }
            isSpawned_playerCrew = true;
            if (isSpawned_EnemyCrew)
            {
                ClientSend.SendFightReady();
            }                
        }

        void OnUseUnitBtn(int _position)
        {        
            ClientSend.AttackPackets(_position,1);              
            
        }

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void SpawnAllEnemyMemberCrew(Dictionary<int, NeokyCollection> _enemyAllCrew)
        {
            isSpawned_EnemyCrew = false;
            foreach (var _crewUnit in _enemyAllCrew)
            {
                // On PlayeCollectionUnit find the MemberCrewPrefab using _memberCrewPrefab Name
                Debug.Log("Find Prefab in PlayerCollectionList");
                //GameObject unitCrewPrefab = PlayerCollectionList.Where<GameObject>(x => x.name == _crewUnit.Value.collection_prefab).SingleOrDefault();

                if (_crewUnit.Value.local_Collection_prefab != null)
                {
                    Transform Spawn = GameObject.Find("Spawn_Enemy_" + _crewUnit.Key.ToString()).transform;
                    // Create Player Prefab
                    Debug.Log("Instantiate Crew Prefab");
                    GameObject CrewMember = Instantiate(_crewUnit.Value.local_Collection_prefab, Spawn);
                    CrewMember.transform.parent = Spawn; // Set the Member Spawn on the Spawn_X position
                                                         //CrewMember.transform.localPosition = new Vector3(0, 0, 0);
                                                         // Initialize Player
                    CrewMember.GetComponent<CrewMembers>().Initialize(_crewUnit.Key, _crewUnit.Value.collection_name, _crewUnit.Value.lifePoints);

                    // Add player to a List of Players
                    Debug.Log("CrewMember added to EnemyCrew");
                    EnemyCrew.Add(_crewUnit.Key, CrewMember);
                }
                else
                {
                    Debug.Log("You forgot to Add the Prefab of the Unit on the GameManager Size on the UnloadScene");
                }
            }
            isSpawned_EnemyCrew = true;
            if (isSpawned_playerCrew)
            {
                ClientSend.SendFightReady();
            }
            
        }

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void UpdateAllPlayerUnits(Dictionary<NeokyCollection, Dictionary<string, int>> _allPlayerUnits)
        {
            AllPlayerUnits = _allPlayerUnits;
            ClientSend.SwitchScene(Constants.SCENE_COLLECTION);
        }

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
       /* public void CallbackPlayerAttack(int _unitPosition, int _enemyPosition)
        {
            GameObject _PlayerUnitGameObject = new GameObject();
            GameObject _EnemyUnitGameObject = new GameObject();

            PlayerCrew.TryGetValue(_unitPosition, out _PlayerUnitGameObject);
            EnemyCrew.TryGetValue(_enemyPosition, out _EnemyUnitGameObject);


            Vector3 attackDir = (_EnemyUnitGameObject.transform.position - _PlayerUnitGameObject.transform.position).normalized;
            var Anim = _PlayerUnitGameObject.GetComponent<Animator>();
            
            //Anim.
        }*/



        public void CallbackPlayerAttack(int _unitPosition, int _enemyPosition) // Action onAttackComplete
        {
            GameObject _PlayerUnitGameObject;
            GameObject _EnemyUnitGameObject;

            PlayerCrew.TryGetValue(_unitPosition, out _PlayerUnitGameObject);
            EnemyCrew.TryGetValue(_enemyPosition, out _EnemyUnitGameObject);

            UnitBattle _UnitBattle = _PlayerUnitGameObject.GetComponent<UnitBattle>();
            _UnitBattle.Attack(_unitPosition, _enemyPosition);


        }


        // Change Panel Image
        // Change Panel Souls

        // instantiate New Panel


        // On PlayeCollectionUnit find the MemberCrewPrefab using _memberCrewPrefab Name
        /*Debug.Log("Find Prefab in PlayerCollectionList");
        GameObject unitCrewPrefab = PlayerCollectionList.Where<GameObject>(x => x.name == _crewUnit.Value.collection_prefab).SingleOrDefault();

        if (unitCrewPrefab != null)
        {
            Transform Spawn = GameObject.Find("Spawn_Enemy_" + _crewUnit.Key.ToString()).transform;
            // Create Player Prefab
            Debug.Log("Instantiate Crew Prefab");
            GameObject CrewMember = Instantiate(unitCrewPrefab, Spawn);
            CrewMember.transform.parent = Spawn; // Set the Member Spawn on the Spawn_X position
                                                 //CrewMember.transform.localPosition = new Vector3(0, 0, 0);
                                                 // Initialize Player
            CrewMember.GetComponent<CrewMembers>().Initialize(_crewUnit.Key, _crewUnit.Value.collection_name, _crewUnit.Value.lifePoints);

            // Add player to a List of Players
            Debug.Log("CrewMember added to EnemyCrew");
            EnemyCrew.Add(_crewUnit.Key, CrewMember);
        }
        else
        {
            Debug.Log("You forgot to Add the Prefab of the Unit on the GameManager Size on the UnloadScene");
        }*/






        #region SCENE_MANAGEMENT
        /// <summary>Spawns a player.</summary>
        /// <param name="_newScene">The New Scene to Switch To</param>
        public void SwitchToScene(string _newScene, string _oldScene)
        {
                if (SceneManager.GetSceneByName(_newScene).isLoaded == false)
                {
                    SceneManager.LoadSceneAsync(_newScene, LoadSceneMode.Additive);                    

                if (_oldScene != Constants.SCENE_SAMESCENE)
                    {
                        if (_oldScene != Constants.SCENE_NOSCENE)
                        {
                            SceneManager.UnloadSceneAsync(_oldScene);
                        }
                    }
                }
                else
                {
                    //SceneManager.UnloadSceneAsync(_newScene);
                }
        }
        #endregion
    }
}