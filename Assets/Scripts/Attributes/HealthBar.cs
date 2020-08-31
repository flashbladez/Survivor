using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;
        [SerializeField] bool isEnabledOutOfCombat = false;
        [SerializeField] Text textDisplay = null;
        //[SerializeField] GameObject Healthbar = null;

        void Update()
        {
            if (healthComponent == null)
            {
                return;
            }
           
            if (Mathf.Approximately(healthComponent.GetFraction(), 0) || Mathf.Approximately(healthComponent.GetFraction(), 1))
            {
                if (!isEnabledOutOfCombat)
                {
                    rootCanvas.enabled = false;
                  //  if (textDisplay != null)
                   // {
                     //   textDisplay.enabled = false;
                  //  }
                    return;
                }
               
            }
            rootCanvas.enabled = true;
          ///  if (textDisplay != null)
          //  {
             //   textDisplay.enabled = true;
          //  }
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1f, 1f);
          
        }

        public void GetHealthRef(Health health)
        {
            healthComponent = health;      
        }
    }
}
