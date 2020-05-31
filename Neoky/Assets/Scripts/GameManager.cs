using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
        public static Dictionary<int, GameObject> PlayerCrew = new Dictionary<int, GameObject>();
        public static Dictionary<int, GameObject> EnemyCrew = new Dictionary<int, GameObject>();

        public GameObject netPlayerPrefab;
        [SerializeField]
        public List<GameObject> PlayerCollectionList;

        
        //public Dictionary<string, GameObject> PlayerCollectionList = new Dictionary<string, GameObject>();


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

        /// <summary>Spawns a player.</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The player's name.</param>
        public void SpawnPlayer(int _id, string _username, float _level, float _levelxp, float _requiredLvlUpXp, string _startScene, string _unloadScene)
        {
            // Create Player Prefab
            GameObject _player = Instantiate(netPlayerPrefab);       
            
            // Initialize Player
            _player.GetComponent<PlayerManager>().Initialize(_id, _username, _level, _levelxp, _requiredLvlUpXp, _startScene);

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
            foreach (var _crewUnit in _playerAllCrew)
            {
                // On PlayeCollectionUnit find the MemberCrewPrefab using _memberCrewPrefab Name
                Debug.Log("Find Prefab in PlayerCollectionList");
                GameObject unitCrewPrefab = PlayerCollectionList.Where<GameObject>(x => x.name == _crewUnit.Value.collection_prefab).SingleOrDefault();

                if (unitCrewPrefab != null)
                {
                    Transform Spawn = GameObject.Find("Spawn_Player_" + _crewUnit.Key.ToString()).transform;
                    // Create Player Prefab
                    Debug.Log("Instantiate Crew Prefab");
                    GameObject CrewMember = Instantiate(unitCrewPrefab, Spawn);
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
            }
            
        }

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void SpawnAllEnemyMemberCrew(Dictionary<int, NeokyCollection> _enemyAllCrew)
        {
            foreach (var _crewUnit in _enemyAllCrew)
            {
                // On PlayeCollectionUnit find the MemberCrewPrefab using _memberCrewPrefab Name
                Debug.Log("Find Prefab in PlayerCollectionList");
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
                }
            }
        }


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