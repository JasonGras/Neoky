using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        //public Transform camTransform;

        private void Update()
        {
           /* if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //ClientSend.PlayerShoot(camTransform.forward);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //ClientSend.PlayerThrowItem(camTransform.forward);
            }*/
        }

        private void FixedUpdate()
        {
            //SendInputToServer();
        }

        /// <summary>Sends player input to the server.</summary>
        /*private void SendInputToServer()
        {
            bool[] _inputs = new bool[]
            {
            Input.GetKey(KeyCode.Z),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.Q),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
            };

            ClientSend.PlayerMovement(_inputs);
        }*/
    }
}