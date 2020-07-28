using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Survivor.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;
        [SerializeField] bool isEnabledOutOfCombat = false;

        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(), 0) || Mathf.Approximately(healthComponent.GetFraction(),1))
            {
                if (!isEnabledOutOfCombat)
                {
                    rootCanvas.enabled = false;
                }
                else
                {
                    rootCanvas.enabled = true;
                }

                return;
            }
            rootCanvas.enabled = true;

            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1f, 1f);

        }
    }
}
