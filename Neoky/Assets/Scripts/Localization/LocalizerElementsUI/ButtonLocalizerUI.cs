using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonLocalizerUI : MonoBehaviour
{
    TMP_Text textBtn;

    public string key;

    // Start is called before the first frame update
    void Start()
    {
        textBtn = GetComponent<Button>().GetComponentInChildren<TMP_Text>();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textBtn.text = value;
    }
}


