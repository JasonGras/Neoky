using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class NeokyCollection
    {
        public string collection_prefab { get; set; }

        public float attackDamages { get; set; }

        public string collection_name { get; set; }

        public float attackSpeed { get; set; }

        public float lifePoints { get; set; }


        public NeokyCollection(string _collectionName, string _collectionPrefab, float _attackDamages, float _attackSpeed, float _lifePoints)
        {
            collection_name = _collectionName;
            collection_prefab = _collectionPrefab;
            attackDamages = _attackDamages;
            attackSpeed = _attackSpeed;
            lifePoints = _lifePoints;
        }
    }
}
