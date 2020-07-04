using Assets.Scripts.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Unit
    {
        public string UnitPrefab { get; set; }

        public GameObject local_Collection_prefab { get; set; }

        public GameObject local_Collection_IMGBtn_prefab { get; set; }

        public Sprite local_Collection_image { get; set; }

        public float UnitDamages { get; set; }

        public string UnitName { get; set; }

        public string UnitImage { get; set; }

        public string UnitTribe { get; set; }

        public float UnitVelocity { get; set; }

        public float UnitLevel { get; set; }

        public float UnitTurnMeter { get; set; }

        public int UnitQuality { get; set; }

        public float UnitHP { get; set; }

        public List<Spell> UnitSpellList { get; set; }

        public GameObject InstantiatedUnit { get; set; }


        public Unit(string _UnitName,float _UnitLevel, string _UnitPrefab, string _UnitImage, float _UnitDamages, float _UnitVelocity, float _UnitHP, float _UnitTurnMeter, int _UnitQuality, List<Spell> _UnitSpellList, string _UnitTribe)
        {
            UnitName = _UnitName;
            UnitLevel = _UnitLevel;
            UnitPrefab = _UnitPrefab;
            UnitImage = _UnitImage;
            UnitDamages = _UnitDamages;
            UnitVelocity = _UnitVelocity;
            UnitHP = _UnitHP;
            UnitTurnMeter = _UnitTurnMeter;
            UnitQuality = _UnitQuality;
            UnitTribe = _UnitTribe;
            UnitSpellList = _UnitSpellList;

            foreach (Spell Spell in _UnitSpellList)
            {
                Spell.Spell_image = Resources.Load<Sprite>("Collection/" + _UnitTribe + "/" + _UnitName + "/Spell/" + Spell.SpellIMG);
            }

            local_Collection_prefab = Resources.Load<GameObject>("Collection/" + _UnitTribe + "/"+ _UnitName + "/"+ _UnitPrefab);
            local_Collection_image = Resources.Load<Sprite>("Collection/" + _UnitTribe + "/" + _UnitName + "/" + _UnitImage);
            local_Collection_IMGBtn_prefab = Resources.Load<GameObject>("Prefab/UnitImageBtn");
            //Debug.Log("prefab Found");
            //collection_prefab = _collectionPrefab;
        }

        private void ConvertToSpell(List<string> _UnitSpellList)
        {
           /* List<ISpell> _currentSpellList = new List<ISpell>();
            
            foreach (var StringLine in _UnitSpellList)
            {
                FindSpellFromID(StringLine)
                ISpell currentSpell;
                currentSpell.SpellID = StringLine;
                _currentSpellList.Add();
            }*/
        }
    }
}
