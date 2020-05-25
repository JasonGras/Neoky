using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Message from server: {_msg}");
            Client.instance.myId = _myId;
            ClientSend.WelcomeReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }

        public static void SpawnPlayer(Packet _packet)
        {
            int _id = _packet.ReadInt();
            string _username = _packet.ReadString();
            float _level = _packet.ReadFloat();
            float _levelxp = _packet.ReadFloat();
            float _requiredLvlUpXp = _packet.ReadFloat();
            string _startScene = _packet.ReadString();
            string _oldScene = _packet.ReadString();

            GameManager.instance.SpawnPlayer(_id, _username, _level, _levelxp, _requiredLvlUpXp,_startScene, _oldScene);
        }

        /*public static void SpawnPlayer(Packet _packet)
        {
            int _id = _packet.ReadInt();
            string _username = _packet.ReadString();
            Vector3 _position = _packet.ReadVector3();
            Quaternion _rotation = _packet.ReadQuaternion();

            GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
        }*/

        public static void SwitchToScene(Packet _packet)
        {
            string _newScene = _packet.ReadString();
            string _oldScene = _packet.ReadString();

            int _myId = _packet.ReadInt();

            Debug.Log($"New Scene Recieved : {_newScene}");

            /*if (GameManager.players.TryGetValue(_myId, out PlayerManager _player))
            {
                _player.currentScene... ?
            }*/
            //ClientSend.WelcomeReceived(); 
            GameManager.instance.SwitchToScene(_newScene, _oldScene);
        }

        public static void RedefineMyPassword(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            // Send my Client to Redefine Password Scene
            GameManager.instance.SwitchToScene(Constants.SCENE_REDEFINEPASSWORD,Constants.SCENE_AUTHENTICATION);
        }

        public static void SignUpReturn(Packet _packet)
        {
            string _returnStatus = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Return Sign Up Status : {_returnStatus}");

            switch (_returnStatus)
            {
                case "ADHESION_ALREADY_EXIST":
                    SignUpScript.SignUpInst.UpdateSceneSignUpErrorMessage(LocalizationSystem.GetLocalizedValue(Constants.sign_up_user_already_exist));
                    break;
                case "ADHESION_OK":
                    Debug.Log("Creation de Compte Finalisée.");
                    SignUpScript.SignUpInst.UpdateSceneSignUpSuccessMessage(LocalizationSystem.GetLocalizedValue(Constants.signup_validation_email_lbl));
                    break;
                case "ADHESION_KO":
                    Debug.Log("Votre création de compte a échoué, merci de réessayer ultérieurement.");
                    break;
                default:
                    Debug.Log("Une erreur technique est survenue, merci de réessayer ultérieurement.");
                    break;
            }
        }

        public static void SignInReturn(Packet _packet)
        {
            string _returnStatus = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Return Sign Up Status : {_returnStatus}");

            switch (_returnStatus)
            {
                case "AUTHENTICATION_OK":
                    Debug.Log("Authentification réussie.");
                    //AccessHomePage();
                    break;
                case "AUTHENTICATION_USER_CONFIRMED_KO":
                    Debug.Log("User Not confirmed, please confirm your email before log In.");
                    Authentication.Auth.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.authentication_non_confirmed_user_lbl));
                    break;
                case "AUTHENTICATION_KO":
                    Debug.Log("Votre authentification a échoué, merci de réessayer ultérieurement.");
                    Authentication.Auth.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.authentication_failed_lbl));
                    break;
                default:
                    Debug.Log("Une erreur technique est survenue, merci de réessayer ultérieurement.");
                    break;
            }
        }
        public static void SignInTokens(Packet _packet)
        {
            UserSession _clientTokens = _packet.ReadUserSession();
            int _myId = _packet.ReadInt();

            Debug.Log($"SignInTokens | User Session Recieved");
            Client.instance.myCurrentSession = _clientTokens;

        }

        /*public static void PlayerPosition(Packet _packet)
        {
            int _id = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
            {
                _player.transform.position = _position;
            }
        }*/
        /*
        public static void PlayerRotation(Packet _packet)
        {
            int _id = _packet.ReadInt();
            Quaternion _rotation = _packet.ReadQuaternion();

            if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
            {
                _player.transform.rotation = _rotation;
            }
        }

        public static void PlayerDisconnected(Packet _packet)
        {
            int _id = _packet.ReadInt();

            Destroy(GameManager.players[_id].gameObject);
            GameManager.players.Remove(_id);
        }

        public static void PlayerHealth(Packet _packet)
        {
            int _id = _packet.ReadInt();
            float _health = _packet.ReadFloat();

            GameManager.players[_id].SetHealth(_health);
        }

        public static void PlayerRespawned(Packet _packet)
        {
            int _id = _packet.ReadInt();

            GameManager.players[_id].Respawn();
        }

        public static void CreateItemSpawner(Packet _packet)
        {
            int _spawnerId = _packet.ReadInt();
            Vector3 _spawnerPosition = _packet.ReadVector3();
            bool _hasItem = _packet.ReadBool();

            GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
        }

        public static void ItemSpawned(Packet _packet)
        {
            int _spawnerId = _packet.ReadInt();

            GameManager.itemSpawners[_spawnerId].ItemSpawned();
        }

        public static void ItemPickedUp(Packet _packet)
        {
            int _spawnerId = _packet.ReadInt();
            int _byPlayer = _packet.ReadInt();

            GameManager.itemSpawners[_spawnerId].ItemPickedUp();
            GameManager.players[_byPlayer].itemCount++;
        }

        public static void SpawnProjectile(Packet _packet)
        {
            int _projectileId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();
            int _thrownByPlayer = _packet.ReadInt();

            GameManager.instance.SpawnProjectile(_projectileId, _position);
            GameManager.players[_thrownByPlayer].itemCount--;
        }

        public static void ProjectilePosition(Packet _packet)
        {
            int _projectileId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            if (GameManager.projectiles.TryGetValue(_projectileId, out ProjectileManager _projectile))
            {
                _projectile.transform.position = _position;
            }
        }

        public static void ProjectileExploded(Packet _packet)
        {
            int _projectileId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            GameManager.projectiles[_projectileId].Explode(_position);
        }

        public static void SpawnEnemy(Packet _packet)
        {
            int _enemyId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            GameManager.instance.SpawnEnemy(_enemyId, _position);
        }

        public static void EnemyPosition(Packet _packet)
        {
            int _enemyId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
            {
                _enemy.transform.position = _position;
            }
        }

        public static void EnemyHealth(Packet _packet)
        {
            int _enemyId = _packet.ReadInt();
            float _health = _packet.ReadFloat();

            GameManager.enemies[_enemyId].SetHealth(_health);
        }*/
    }
}