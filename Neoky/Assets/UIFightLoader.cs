using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class UIFightLoader : MonoBehaviour
    {

        public Color defaultColour;
        public Color selectedColour;

        public Material mat;

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
        }
        

        void OnTouchDown()
        {
            mat.color = selectedColour;
        }
        void OnTouchUp()
        {
            mat.color = defaultColour;
        }
        void OnTouchStay()
        {
            mat.color = selectedColour;
        }
        void OnTouchExit()
        {
            mat.color = defaultColour;
        }
    }
}
