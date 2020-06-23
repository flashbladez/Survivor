using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;

        void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }


        void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}" ,baseStats.Getlevel());
        }
    }
}
