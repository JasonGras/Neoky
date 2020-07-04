using Assets.Scripts.Spells;
using JetBrains.Annotations;
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
        //public static Dictionary<int, GameObject> PlayerObjCrew = new Dictionary<int, GameObject>();
        //public static Dictionary<int, GameObject> EnemyObjCrew = new Dictionary<int, GameObject>();

        public static Dictionary<int, Unit> PlayerUnitCrew ;
        public static Dictionary<int, Unit> IAUnitCrew ;

        public static Dictionary<Unit, Dictionary<string,int>> AllPlayerUnits = new Dictionary<Unit, Dictionary<string, int>>();
        //public static List<NeokyCollection> AllGameUnits = new List<NeokyCollection>();

        public GameObject netPlayerPrefab;

        private Transform THorizontalBtnLayer;

        //[SerializeField]
        //public List<GameObject> PlayerCollectionList;

        public bool isSpawned_playerCrew { get; set; } = false;
        public bool isSpawned_IACrew { get; set; } = false;

        public enum SpellTarget
        {
            ALLY,
            IAUNIT,
            SELF,
        }

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
        public void SpawnAllPlayerMemberCrew(Dictionary<int, Unit> _playerAllCrew)
        {
            PlayerUnitCrew = new Dictionary<int, Unit>();
            PlayerUnitCrew = _playerAllCrew;

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
                    CrewMember.GetComponent<CrewMembers>().Initialize(_crewUnit.Key, _crewUnit.Value.UnitName, _crewUnit.Value.UnitHP);

                    // Add player to a List of Players
                    Debug.Log("CrewMember added to PlayerCrew");
                    _crewUnit.Value.InstantiatedUnit = CrewMember;
                    //PlayerObjCrew.Add(_crewUnit.Key, CrewMember);
                }
                else
                {
                    Debug.Log("You forgot to Add the Prefab of the Unit on the GameManager Size on the UnloadScene");
                }


                

            }
            isSpawned_playerCrew = true;
            if (isSpawned_IACrew)
            {
                ClientSend.SendFightReady();
            }                
        }

        void OnUseUnitBtn(int _position)
        {
            //Debug.Log("Listener Clicked " + _position);
            ClientSend.AttackPackets(_position,1);              
            
        }

        void OnUseSpell(Spell _Spell, int _PlayerUnitID)
        {
            //Debug.Log("Listener Clicked " + _position);
            //ClientSend.AttackPackets(_position, 1);
            int Target = 1;
            ClientSend.AttackSpell(_Spell.SpellID, Target, _PlayerUnitID);

        }
        

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void SpawnAllEnemyMemberCrew(Dictionary<int, Unit> _enemyAllCrew)
        {
            IAUnitCrew = new Dictionary<int, Unit>();
            IAUnitCrew = _enemyAllCrew;

            isSpawned_IACrew = false;
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
                    CrewMember.GetComponent<CrewMembers>().Initialize(_crewUnit.Key, _crewUnit.Value.UnitName, _crewUnit.Value.UnitHP);

                    // Add player to a List of Players
                    Debug.Log("CrewMember added to EnemyCrew");
                    _crewUnit.Value.InstantiatedUnit = CrewMember;
                    //EnemyObjCrew.Add(_crewUnit.Key, CrewMember);
                }
                else
                {
                    Debug.Log("You forgot to Add the Prefab of the Unit on the GameManager Size on the UnloadScene");
                }
            }
            isSpawned_IACrew = true;
            if (isSpawned_playerCrew)
            {
                ClientSend.SendFightReady();
            }
            
        }

        /// <summary>Spawns All player Crew</summary>
        /// <param name="_id">The player's ID.</param>
        /// <param name="_name">The member crew ID.</param>
        public void UpdateAllPlayerUnits(Dictionary<Unit, Dictionary<string, int>> _allPlayerUnits)
        {
            AllPlayerUnits = _allPlayerUnits;
            ClientSend.SwitchScene(Constants.SCENE_COLLECTION);
        }

        public void SetNewPlayerUnitTurn(int _NewUnitIDTurn)
        {
            Unit _NewtUnit;

            // Get the parent Location
            THorizontalBtnLayer = GameObject.Find("HorizontalBtnLayer").transform;

            foreach (Transform child in THorizontalBtnLayer)
            {
                GameObject.Destroy(child.gameObject);
            }

            // Instantiate the Btn Image
            PlayerUnitCrew.TryGetValue(_NewUnitIDTurn, out _NewtUnit);
            //UnitBattle _PlayerUnitBattle = _NewtUnit.InstantiatedUnit.GetComponent<UnitBattle>();

            // Foreach Spell
            foreach (var Spell in _NewtUnit.UnitSpellList)
            {     
                    GameObject CrewMemberImgBtn = Instantiate(_NewtUnit.local_Collection_IMGBtn_prefab, THorizontalBtnLayer);
                    // Set the Image of the Button
                    Image CrewUnitImageBtn = CrewMemberImgBtn.GetComponent<Image>();

                    CrewUnitImageBtn.sprite = Spell.Spell_image; // Put the Image of The Spell ? 

                    CrewMemberImgBtn.GetComponent<Button>().onClick.AddListener(() => OnUseSpell(Spell, _NewUnitIDTurn));                
            }
            

            

            
            //Debug.Log("CrewMemberImgBtn set Up " + _crewUnit.Key);
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
            Unit _PlayerUnitGameObject;
            Unit _EnemyUnitGameObject;

            PlayerUnitCrew.TryGetValue(_unitPosition, out _PlayerUnitGameObject);
            IAUnitCrew.TryGetValue(_enemyPosition, out _EnemyUnitGameObject);

            UnitBattle _UnitBattle = _PlayerUnitGameObject.InstantiatedUnit.GetComponent<UnitBattle>();

        }

        public void PlayerUnitAttack(int PlayerAttackingUnit, int _TargetAttackedUnit, SpellTarget TargetPlayerOrIA, string SpellID)
        {
            Unit _PlayerUnitGameObject;

            switch (TargetPlayerOrIA)
            {
                
                case SpellTarget.IAUNIT:
                    PlayerUnitCrew.TryGetValue(PlayerAttackingUnit, out _PlayerUnitGameObject);
                    UnitBattle _PlayerUnitBattle = _PlayerUnitGameObject.InstantiatedUnit.GetComponent<UnitBattle>();
                    _PlayerUnitBattle.PlayerSpellAttack(SpellID,_TargetAttackedUnit, PlayerAttackingUnit);
                    break;
                case SpellTarget.ALLY:
                case SpellTarget.SELF:
                default:
                    PlayerUnitCrew.TryGetValue(PlayerAttackingUnit, out _PlayerUnitGameObject);
                    UnitBattle _PlayerUnitBattleBuff = _PlayerUnitGameObject.InstantiatedUnit.GetComponent<UnitBattle>();
                    _PlayerUnitBattleBuff.PlayerSpellBuff(SpellID, _TargetAttackedUnit, PlayerAttackingUnit);
                    break;
            }           
        }
        public void IAUnitAttack(int IAAttackingUnit, int _TargetAttackedUnit, SpellTarget TargetPlayerOrIA, string SpellID)
        {
            Unit _IAUnitGameObject;

            switch (TargetPlayerOrIA)
            {
                
                case SpellTarget.IAUNIT:
                    IAUnitCrew.TryGetValue(IAAttackingUnit, out _IAUnitGameObject);
                    UnitBattle _IAUnitBattle = _IAUnitGameObject.InstantiatedUnit.GetComponent<UnitBattle>();
                    _IAUnitBattle.IASpellAttack(SpellID,_TargetAttackedUnit, IAAttackingUnit);
                    break;
                case SpellTarget.ALLY:
                case SpellTarget.SELF:
                default:                
                    /*EnemyUnitCrew.TryGetValue(IAAttackingUnit, out _UnitGameObject);
                    UnitBattle _IAUnitBattle = _UnitGameObject.InstantiatedUnit.GetComponent<UnitBattle>();
                    _IAUnitBattle.IAAttack(_TargetAttackedUnit);*/
                    break;
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