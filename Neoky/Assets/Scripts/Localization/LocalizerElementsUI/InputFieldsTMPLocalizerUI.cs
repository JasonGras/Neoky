using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldsTMPLocalizerUI : MonoBehaviour
{
    TMP_Text textBtn;

    public string key;

    // Start is called before the first frame update
    void Start()
    {

        textBtn = GetComponent<TMP_InputField>().GetComponentInChildren<TMP_Text>(); ;
        string value = LocalizationSystem.GetLocalizedValue(key);
        textBtn.text = value;
    }
}
