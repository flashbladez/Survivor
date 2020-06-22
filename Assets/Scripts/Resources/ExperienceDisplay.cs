using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }


        void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}" ,experience.GetPoints());
        }
    }
}
