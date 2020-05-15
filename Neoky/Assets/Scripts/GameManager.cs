using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
        //public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
        //public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
        //public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();

        //public GameObject localPlayerPrefab;
        //public GameObject playerPrefab;
        public GameObject netPlayerPrefab;
        public GameObject netOtherPlayerPrefab;
        //public GameObject itemSpawnerPrefab;
        //public GameObject projectilePrefab;
        //public GameObject enemyPrefab;

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
        public void SpawnPlayer(int _id, string _startScene, string _oldScene)
        {
            GameObject _player;
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(netPlayerPrefab);
            }
            else
            {
                _player = Instantiate(netOtherPlayerPrefab);
            }            
            _player.GetComponent<PlayerManager>().Initialize(_id,_startScene);
            players.Add(_id, _player.GetComponent<PlayerManager>());
            SwitchToScene(_id, _startScene, _oldScene);

        }

        /// <summary>Spawns a player.</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The player's name.</param>
        /// <param name="_position">The player's starting position.</param>
        /// <param name="_rotation">The player's starting rotation.</param>
        /*public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
        {
            GameObject _player;
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(localPlayerPrefab, _position, _rotation);
            }
            else
            {
                _player = Instantiate(playerPrefab, _position, _rotation);
            }

            _player.GetComponent<PlayerManager>().Initialize(_id, _username);
            players.Add(_id, _player.GetComponent<PlayerManager>());
        }*/

        /// <summary>Spawns a player.</summary>
        /// <param name="_newScene">The New Scene to Switch To</param>

        public void SwitchToScene(int _id, string _newScene, string _oldScene)
        {
            if (_id == Client.instance.myId)
            {                
                //SceneManager.LoadScene(_newScene);
                if (SceneManager.GetSceneByName(_newScene).isLoaded == false)
                {
                    SceneManager.LoadSceneAsync(_newScene, LoadSceneMode.Additive);
                    players[Client.instance.myId].currentScene = _newScene; // A titre d'information on modifie la CurrentScene variable de notre player                    
                    //Debug.Log("My player current scene :"+players[Client.instance.myId].currentScene);
                    if (_oldScene != Constants.SCENE_NOSCENE)
                    {
                        SceneManager.UnloadSceneAsync(_oldScene);
                    }                    
                }
                else
                {
                    SceneManager.UnloadSceneAsync(_newScene);
                }
                //UIManager.instance.collectionButton.interactable = true;
            }
        }
        /*
        public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem)
        {
            GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
            _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
            itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());
        }

        public void SpawnProjectile(int _id, Vector3 _position)
        {
            GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
            _projectile.GetComponent<ProjectileManager>().Initialize(_id);
            projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
        }

        public void SpawnEnemy(int _id, Vector3 _position)
        {
            GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
            _enemy.GetComponent<EnemyManager>().Initialize(_id);
            enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
        }*/
    }
}