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

        public static void AccessHomePage()
        {
            using (Packet _packet = new Packet((int)ClientPackets.accessHomePage))
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