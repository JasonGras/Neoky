using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        public int id;
        public string username;
        //public float health;
        //public float maxHealth = 100f;
        //public int itemCount = 0;
        //public MeshRenderer model;

        public string currentScene;

        public void Initialize(int _id, string _startScene)
        {
            id = _id;
            //username = _username;
            //health = maxHealth;
            currentScene = _startScene;
        }

        /*public void SetHealth(float _health)
        {
            health = _health;

            if (health <= 0f)
            {
                Die();
            }
        }

        public void Die()
        {
            model.enabled = false;
        }

        public void Respawn()
        {
            model.enabled = true;
            SetHealth(maxHealth);
        }*/
    }
}