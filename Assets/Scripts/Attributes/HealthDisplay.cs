using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
       // [SerializeField] Text healthDisplayPercent;
        Health health;

        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }


        void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}" ,health.GetHealthPoints(),health.GetMaxHealthPoints());
        }
    }
}
