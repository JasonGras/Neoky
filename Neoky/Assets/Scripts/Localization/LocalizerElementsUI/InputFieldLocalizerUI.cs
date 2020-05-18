using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldLocalizerUI : MonoBehaviour
{
        Text textBtn;

        public string key;

        // Start is called before the first frame update
        void Start()
        {

            textBtn = GetComponent<InputField>().GetComponentInChildren<Text>();
            string value = LocalizationSystem.GetLocalizedValue(key);
            textBtn.text = value;
        }   
}
