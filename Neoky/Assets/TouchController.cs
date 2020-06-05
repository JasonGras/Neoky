using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TouchController : MonoBehaviour
    {
        //public LayerMask touchInputMask;
        public ParticleSystem emitter;
        public float distFromCam = 10; //distance of the emitter from the camera

        //private List<GameObject> touchList = new List<GameObject>(); // Will permit to compare old lists
        //private GameObject[] touchesOld;

        //private RaycastHit hit;

        void Update()
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distFromCam));
                        emitter.transform.position = pos;
                        emitter.Play();
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        //recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        //recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled) // Ipad on Face or 6 figers
                    {
                        //recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

               /* if (Input.touchCount > 0)
            {
                touchesOld = new GameObject[touchList.Count];
                touchList.CopyTo(touchesOld);
                touchList.Clear();

                foreach (Touch touch in Input.touches)
                {
                    Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);                    

                    if (Physics.Raycast(ray, out hit, touchInputMask))
                    {
                        //If we hit an Object we want the reference of Object
                        GameObject recipient = hit.transform.gameObject;
                        touchList.Add(recipient);


                        if (touch.phase == TouchPhase.Began)
                        {
                            recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Ended)
                        {
                            recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                        {
                            recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Canceled) // Ipad on Face or 6 figers
                        {
                            recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                }
                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g))
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }

            }*/
        }
        
    }
}
