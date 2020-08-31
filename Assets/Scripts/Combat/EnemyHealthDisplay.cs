using Survivor.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] GameObject enemyHealthBar = null;
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
           // GetComponent<Text>().enabled = false;
        }
                
        void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<Text>().enabled = false;
                return;
            }
            GetComponent<Text>().enabled = true;
            Health health = fighter.GetTarget();
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            enemyHealthBar.GetComponent<HealthBar>().GetHealthRef(health);
        }
    }
}
