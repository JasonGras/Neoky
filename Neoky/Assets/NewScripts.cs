using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<Button>().onClick.AddListener(() => OnUseUnitBtn(2));
    }

    void OnUseUnitBtn(int x)
    {
        Debug.Log(x);
    }
}
