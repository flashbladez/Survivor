using Survivor.Combat;
using Survivor.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Inventories
{
    public class TooltipStatDisplay : MonoBehaviour
    {
       
        void Start()
        {
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(Stat.Health))
                {
                     //var stat = WeaponConfig.GetAdditiveModifiers(Stat.Health);
                     GetComponent<Text>().text = string.Format("{0:0}", modifier);
                }
            }

            
        }
  
    }
}
