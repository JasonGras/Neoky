﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        public int id;
        public string username;
        public float level;
        public float levelXp;
        public float requiredLvlUpXp;
        public float golds;
        [SerializeField]
        public Dictionary<string,int> coin;
        public float diams;

        

        //public float health;
        //public float maxHealth = 100f;
        //public int itemCount = 0;
        //public MeshRenderer model;

        public string currentScene;

        public void Initialize(int _id, string _username, float _level, float _levelxp, float _requiredLvlUpXp, string _startScene, float _golds, Dictionary<string, int> _coin, float _diams)
        {
            id = _id;
            username = _username;
            level = _level;
            levelXp = _levelxp;
            requiredLvlUpXp = _requiredLvlUpXp;
            currentScene = _startScene;
            golds = _golds;
            coin = _coin;
            diams = _diams;
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