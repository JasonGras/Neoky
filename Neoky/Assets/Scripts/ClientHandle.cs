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
            string _unloadScene = _packet.ReadString();
            float _golds = _packet.ReadFloat();
            int _dicoCount = _packet.ReadInt();
            Dictionary<string,int> _coin = _packet.ReadDicoStringInt(_dicoCount);
            float _diams = _packet.ReadFloat();

            GameManager.instance.SpawnPlayer(_id, _username, _level, _levelxp, _requiredLvlUpXp,_startScene, _unloadScene, _golds, _coin, _diams);
        }       

        public static void SpawnTheEnemyAllCrewMember(Packet _packet)
        {
            Dictionary<int, Unit> _AllCrewPosition = new Dictionary<int, Unit>();

            int _id = _packet.ReadInt();
            int _enemyCount = _packet.ReadInt();
            _AllCrewPosition = _packet.ReadEnemyCrew(_enemyCount);            

            Debug.Log("New EnemyAllCrew recieved !");
            GameManager.instance.SpawnAllEnemyMemberCrew(_AllCrewPosition);

            _AllCrewPosition.Clear();
        }

        public static void AttackCallback(Packet _packet)
        {
            //Dictionary<NeokyCollection, Dictionary<string, int>> _AllPlayerUnits = new Dictionary<NeokyCollection, Dictionary<string, int>>();

            int _id = _packet.ReadInt();
            int _unitPlayerPosition = _packet.ReadInt();
            int _unitEnemyPosition = _packet.ReadInt();

            Debug.Log("Player N° " + _unitPlayerPosition.ToString() + " Attacked Enemy N° " + _unitEnemyPosition.ToString());
            //GameManager.PlayerCrew.TryGetValue(_unitPlayerPosition, out _PlayerUnitGameObject);
            GameManager.instance.CallbackPlayerAttack(_unitPlayerPosition, _unitEnemyPosition);

            //_AllPlayerUnits.Clear();
        }
        

        public static void GetAllPlayerCollection(Packet _packet)
        {
            Dictionary<Unit, Dictionary<string, int>> _AllPlayerUnits = new Dictionary<Unit, Dictionary<string, int>>();

            int _id = _packet.ReadInt();
            int _unitsCount = _packet.ReadInt();
            int _unitsStatCount = _packet.ReadInt();
            _AllPlayerUnits = _packet.ReadAllPlayerUnits(_unitsCount, _unitsStatCount);

            Debug.Log("Player Collection recieved !");
            GameManager.instance.UpdateAllPlayerUnits(_AllPlayerUnits);

            //_AllPlayerUnits.Clear();
        }

        public static void SpawnThePlayerAllCrewMember(Packet _packet)
        {
            Dictionary<int, Unit> _AllCrewPosition = new Dictionary<int, Unit>();

            int _id = _packet.ReadInt();
            int _crewCount = _packet.ReadInt();
            _AllCrewPosition = _packet.ReadEnemyCrew(_crewCount);

            Debug.Log("New PlayerAllCrew recieved !");
            GameManager.instance.SpawnAllPlayerMemberCrew(_AllCrewPosition);

            _AllCrewPosition.Clear();
        }

        public static void SwitchToScene(Packet _packet)
        {
            string _newScene = _packet.ReadString();
            string _oldScene = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"New Scene Recieved : {_newScene}");
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

        public static void ForgotPasswordReturn(Packet _packet)
        {
            string _returnStatus = _packet.ReadString();
            int _myId = _packet.ReadInt();

            switch (_returnStatus)
            {
                case "FORGOT_PASSWORD_CONFIRMED":
                    Debug.Log("Votre nouveau mot de passe a bien été mis a jours.");
                    ForgotPasswordScript.ForgotPwd.UpdateSuccessSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.forgot_password_success_lbl));
                    ForgotPasswordScript.ForgotPwd.UpdateMyFinalForm();
                    break;
                case "FORGOT_PASSWORD_CODE_EXPIRED_KO":
                    ForgotPasswordScript.ForgotPwd.UpdateErrorSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.forgot_password_code_expire_lbl));
                    break;
                case "FORGOT_PASSWORD_CODE_MISMATCH_KO":
                    ForgotPasswordScript.ForgotPwd.UpdateErrorSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.forgot_password_code_mismatch_lbl));
                    break;
                case "NEW_CODE_SEND_EMAIL":
                    ForgotPasswordScript.ForgotPwd.UpdateMyForm();
                    ForgotPasswordScript.ForgotPwd.UpdateSuccessSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.code_send_success_lbl));
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
                    break;
                case "AUTHENTICATION_USER_CONFIRMED_KO":
                    Debug.Log("User Not confirmed, please confirm your email before log In.");
                    Authentication.Auth.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.authentication_non_confirmed_user_lbl));
                    break;
                case "AUTHENTICATION_KO_RESET_PWD_REQUIRED":
                    Debug.Log("Votre authentification a échoué, merci de réessayer ultérieurement.");
                    Authentication.Auth.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.sign_in_redefine_pwd_required));
                    break;
                case "AUTHENTICATION_KO":
                    Debug.Log("Votre authentification a échoué, merci de réessayer ultérieurement.");
                    Authentication.Auth.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.authentication_failed_lbl));
                    break; 
                case "AUTHENTICATION_REDEFINE_PWD_KO":
                    Debug.Log("Votre authentification a échoué depuis la page Redefine PWD, merci de réessayer ultérieurement.");
                    RedefinePasswordScript.RedefinePwd.UpdateSceneMessage(LocalizationSystem.GetLocalizedValue(Constants.authentication_failed_lbl));
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
        
    }
}