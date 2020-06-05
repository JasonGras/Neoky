using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InstantiateCollectionScript : MonoBehaviour
    {

        public GameObject _panel;
        //public Image _imageUnit;
        //public Image _progressImgUnit;
        //public TMP_Text _progressTextUnit;

        

        // Start is called before the first frame update
        void Start()
        {
            CreateAllUnitPanel(GameManager.AllPlayerUnits);
        }

        public void CreateAllUnitPanel(Dictionary<string, Dictionary<string,int>> Collection)
        {
            foreach (var collectionUnit in Collection)
            {
                GameObject NewPanel;

                // Find my Image on my Panel Prefab
                Image img_panel = _panel.gameObject.transform.Find("Image_Unit").GetComponent<Image>();
                // Set my Image on my Panel Prefab
                img_panel.sprite = Resources.Load<Sprite>("Collection/" + collectionUnit.Key);
                foreach (var collectionUnitDetail in collectionUnit.Value)
                {
                    try
                    {
                        switch (collectionUnitDetail.Key)
                        {
                            case "collection_id_lvl":
                                //_progressTextUnit.text = collectionUnitDetail.Value.ToString();
                                break;
                            case "collection_souls":
                                Transform _PanelProgressTransform = _panel.gameObject.transform.Find("Panel_Progress");
                                TMP_Text _progressTextUnit = _PanelProgressTransform.Find("Text_FillAmount (TMP)").GetComponent<TMP_Text>();
                                _progressTextUnit.text = collectionUnitDetail.Value.ToString();

                                Image _progressImgUnit = _PanelProgressTransform.Find("Fill_Green").GetComponent<Image>();
                                _progressImgUnit.fillAmount = (float)collectionUnitDetail.Value / 100;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Proper Method failed with the following exception: ");
                        Debug.Log(e);
                    }                    
                }
                NewPanel = Instantiate(_panel);
                NewPanel.transform.SetParent(this.transform);
            }

        }

    }
}