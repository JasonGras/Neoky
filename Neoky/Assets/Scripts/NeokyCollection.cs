using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NeokyCollection
    {
        public string collection_prefab { get; set; }

        public GameObject local_Collection_prefab { get; set; }

        public GameObject local_Collection_IMGBtn_prefab { get; set; }

        public Sprite local_Collection_image { get; set; }

        public float attackDamages { get; set; }

        public string collection_name { get; set; }

        public string collection_image { get; set; }

        public float attackSpeed { get; set; }

        public float lifePoints { get; set; }


        public NeokyCollection(string _collectionName, string _collectionPrefab, string _collectionImage, float _attackDamages, float _attackSpeed, float _lifePoints)
        {
            collection_name = _collectionName;
            collection_prefab = _collectionPrefab;            
            collection_image = _collectionImage;
            attackDamages = _attackDamages;
            attackSpeed = _attackSpeed;
            lifePoints = _lifePoints;

            local_Collection_prefab = Resources.Load<GameObject>("Collection/"+ _collectionName+"/"+ _collectionPrefab);
            local_Collection_image = Resources.Load<Sprite>("Collection/" + _collectionName + "/" + _collectionImage);
            local_Collection_IMGBtn_prefab = Resources.Load<GameObject>("Prefab/UnitImageBtn");
            //Debug.Log("prefab Found");
            //collection_prefab = _collectionPrefab;
        }
    }
}
