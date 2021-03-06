﻿using Assets.Scripts.CoinLoots;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ClientSend : MonoBehaviour
    {
        /// <summary>Sends a packet to the server via TCP.</summary>
        /// <param name="_packet">The packet to send to the sever.</param>
        private static void SendTCPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to the server via UDP.</summary>
        /// <param name="_packet">The packet to send to the sever.</param>
        private static void SendUDPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.udp.SendData(_packet);
        }

        #region Packets
        /// <summary>Lets the server know that the welcome message was received.</summary>
        public static void WelcomeReceived()
        {
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.instance.myId);
                //_packet.Write(UIManager.instance.usernameField.text);

                SendTCPData(_packet);
            }
        }


        public static void SendFightReady()
        {
            using (Packet _packet = new Packet((int)ClientPackets.FightPacket))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write("FIGHT_READY");//Desired Scene Name

                Debug.Log($"Fight READY Packets sent");
                SendTCPData(_packet);
            }

        }

        public static void TurnOverPacket(int CurrentUnitID, string TurnOverSide)
        {
            using (Packet _packet = new Packet((int)ClientPackets.TurnOverPacket))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(TurnOverSide);//Desired Scene Name
                _packet.Write(CurrentUnitID);//Desired Scene Name

                Debug.Log($"Turn Over Packet sent for "+ TurnOverSide + " ID : "+ CurrentUnitID);
                SendTCPData(_packet);
            }

        }

        /// <summary>Sends player input to the server.</summary>
        /// <param name="_inputs"></param>
        public static void UnitAttackRequest(Vector3 _TouchInputs)
        {
            using (Packet _packet = new Packet((int)ClientPackets.unitAttack))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write("Test"); //Client.instance.myCurrentSession

                /*_packet.Write(_inputs.Length);
                foreach (bool _input in _inputs)
                {
                    _packet.Write(_input);
                }
                _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);*/

                SendTCPData(_packet);
            }
        }
        /// <summary>Sends player input to the server.</summary>
        /// <param name="_inputs"></param>
        /*public static void PlayerMovement(bool[] _inputs)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
            {
                _packet.Write(_inputs.Length);
                foreach (bool _input in _inputs)
                {
                    _packet.Write(_input);
                }
                _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

                SendUDPData(_packet);
            }
        }*/
        /*public static void TEST(string _scenes)
        {
            using (Packet _packet = new Packet((int)ClientPackets.switchScene))
            {
                _packet.Write(_scenes);//Desired Scene Name

                Debug.Log($"Desired New Scene: {_scenes}");
                SendTCPData(_packet);
            }
        }*/

        /// <summary>Sends player desired Scene to the server.</summary>
        public static void SwitchScene(string _scenes)
        {
            using (Packet _packet = new Packet((int)ClientPackets.switchScene))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(_scenes);//Desired Scene Name

                Debug.Log($"Desired New Scene: {_scenes}");
                SendTCPData(_packet);
            }
        }

        public static void EnterDungeon(string _DungeonName)
        {
            using (Packet _packet = new Packet((int)ClientPackets.enterDungeon))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(_DungeonName);//Desired Scene Name

                Debug.Log($"Desired enter Dungeon: {_DungeonName}");
                SendTCPData(_packet);
            }
        }

        public static void OpenCoin(Coin _coin, CoinAverageQuality _coinQuality)//string _CoinType, string _CoinColor
        {
            using (Packet _packet = new Packet((int)ClientPackets.openCoin))
            {
                _packet.Write(Client.instance.myId);                
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(Convert.ToInt32(_coin));//Desired Scene Name
                _packet.Write(Convert.ToInt32(_coinQuality));//Desired Scene Name

                SendTCPData(_packet);
            }
        }

        public static void UpdatePlayerCollection()
        {
            using (Packet _packet = new Packet((int)ClientPackets.updateCollection))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);

                Debug.Log($"UpdatePlayerCollection");
                SendTCPData(_packet);
            }
        }

        public static void AttackPackets(int _myUnit,int _enemyUnit)
        {
            using (Packet _packet = new Packet((int)ClientPackets.attackPacket))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(_myUnit);//Desired Scene Name
                _packet.Write(_enemyUnit);//Desired Scene Name

                //Debug.Log($"Fight Packets on my Scene: {_status}");
                SendTCPData(_packet);
            }
        }
        
        public static void AttackSpell(string _UnitSpellID, int _IAUnit, int _PlayerUnit)
        {
            using (Packet _packet = new Packet((int)ClientPackets.attackSpellPacket))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(_UnitSpellID);//Desired Scene Name
                _packet.Write(_IAUnit);//Desired Scene Name
                _packet.Write(_PlayerUnit);//Desired Scene Name

                //Debug.Log($"Fight Packets on my Scene: {_status}");
                SendTCPData(_packet);
            }
        }

        public static void FightPackets(string _status)
        {
            using (Packet _packet = new Packet((int)ClientPackets.FightPacket))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                _packet.Write(_status);//Desired Scene Name

                Debug.Log($"Fight Packets on my Scene: {_status}");
                SendTCPData(_packet);
            }
        }

        public static void RedefinedUserPassword(string _username, string _currentPassword, string _newPassword)
        {
            using (Packet _packet = new Packet((int)ClientPackets.redefinedPwd))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(_username);
                _packet.Write(_currentPassword);
                _packet.Write(_newPassword);

                Debug.Log("ClientSend.cs | RedefinedUserPassword Packet Sent");
                SendTCPData(_packet);
            }
        }

        public static void ForgotUserPassword(string _username, string _code,string _newPwd)
        {
            using (Packet _packet = new Packet((int)ClientPackets.forgotPwd))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(_username);
                _packet.Write(_code);
                _packet.Write(_newPwd);

                Debug.Log("ClientSend.cs | ForgotUserPassword Packet Sent");
                SendTCPData(_packet);
            }
        }
        public static void ForgotPasswordRequest(string _username)
        {
            using (Packet _packet = new Packet((int)ClientPackets.forgotPwdRequest))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(_username);

                Debug.Log("ClientSend.cs | ForgotPasswordRequest Packet Sent");
                SendTCPData(_packet);
            }
        }

        public static string SignUpToCognito(string _username, string _password, string _email)
        {
            if(Client.instance.tcp.socket.Connected && Client.instance.myId != 0 )
            {
                using (Packet _packet = new Packet((int)ClientPackets.signUp))
                {
                    _packet.Write(Client.instance.myId);
                    _packet.Write(_username);//Desired Scene Name
                    _packet.Write(_password);//Desired Scene Name
                    _packet.Write(_email);//Desired Scene Name

                    //Debug.Log($"Desired New Scene: {_scenes}");
                    SendTCPData(_packet);
                    return "SIGN_UP_SEND_OK";
                }
            }
            else
            {
                Debug.LogError("TCP Connexion not established");
                return "SIGN_UP_SEND_KO";
            }
            
        }

        public static void LogInToCognito(string _username, string _password)
        {
            using (Packet _packet = new Packet((int)ClientPackets.signIn))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(_username);
                _packet.Write(_password);

                //Debug.Log($"Desired New Scene: {_scenes}");
                SendTCPData(_packet);
            }
        }

        public static void isStillAuthenticated()
        {
            using (Packet _packet = new Packet((int)ClientPackets.stillAuthenticated))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(Client.instance.myCurrentSession);
                SendTCPData(_packet);
            }
        }

        /*public static void PlayerShoot(Vector3 _facing)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
            {
                _packet.Write(_facing);

                SendTCPData(_packet);
            }
        }

        public static void PlayerThrowItem(Vector3 _facing)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerThrowItem))
            {
                _packet.Write(_facing);

                SendTCPData(_packet);
            }
        }*/
        #endregion
    }
}