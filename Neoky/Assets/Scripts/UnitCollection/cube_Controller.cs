using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class cube_Controller : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Touch Phase = Begin End Moved Stationary Canceled
                // Touch.Position = Screen Coordinate Pixel  // Prefab = World Space Coordinates 

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                //Use animation on touchPosition

            }
            //ClientSend.UnitMoove(_inputs);
        }
    }
}