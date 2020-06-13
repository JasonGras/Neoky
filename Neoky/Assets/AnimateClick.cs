using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class AnimateClick : MonoBehaviour
    {
        public ParticleSystem particleSystem;

        // Update is called once per frame
        void Update()
        {
            /*Touch touch = Input.GetTouch(0);

            // Touch Phase = Begin End Moved Stationary Canceled
            // Touch.Position = Screen Coordinate Pixel  // Prefab = World Space Coordinates 

            //if(touch.phase = )
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;*/


            if (Input.GetMouseButtonDown(0))
            {
                RunParticleClick();
                
            }
        }

        private void RunParticleClick()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(ray.origin.x, 0, ray.origin.z));
            particleSystem.transform.position = pos;
            particleSystem.Play();
        }
    }
}