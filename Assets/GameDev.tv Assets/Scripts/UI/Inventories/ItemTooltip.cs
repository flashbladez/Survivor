using UnityEngine;
using TMPro;
using GameDevTV.Inventories;
using Survivor.Stats;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameDevTV.UI.Inventories
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
      
    public class ItemTooltip : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;
              

         public void Setup(InventoryItem item)
         {
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();
            
         }

    }
    
}
