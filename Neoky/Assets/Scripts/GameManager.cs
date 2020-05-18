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

            // Create Player Prefab
            GameObject _player = Instantiate(netPlayerPrefab);       
            
            // Initialize Player
            _player.GetComponent<PlayerManager>().Initialize(_id,_startScene);

            // Add player to a List of Players
            players.Add(_id, _player.GetComponent<PlayerManager>());

            // Send player to his Loged In Homepage
            SwitchToScene(_startScene, _oldScene);

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

        public void LoadScene(string _scene)
        {
            if (SceneManager.GetSceneByName(_scene).isLoaded == false)
            {
                SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);
            }
        }

        public void UnloadScene(string _scene)
        {
            SceneManager.UnloadSceneAsync(_scene);
        }
        #endregion
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