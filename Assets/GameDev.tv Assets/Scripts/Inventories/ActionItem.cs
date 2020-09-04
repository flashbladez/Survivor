using Survivor.Attributes;
using System;
using UnityEngine;

namespace GameDevTV.Inventories
{
    public enum ActionItemType
    {
        Spell,
        Healing,
        Mana,
        Scroll
    }
    /// <summary>
    /// An inventory item that can be placed in the action bar and "Used".
    /// </summary>
    /// <remarks>
    /// This class should be used as a base. Subclasses must implement the `Use`
    /// method.
    /// </remarks>
    /// 
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        // CONFIG DATA
        [Tooltip("Does an instance of this item get consumed every time it's used.")]
        [SerializeField] bool consumable = false;
        [SerializeField] ActionItemType actionItemType;
        // PUBLIC

        /// <summary>
        /// Trigger the use of this item. Override to provide functionality.
        /// </summary>
        /// <param name="user">The character that is using this action.</param>
        public virtual void Use(GameObject user)
        {

            if (actionItemType.ToString() == "Healing")
            {
                user.GetComponent<Health>().Heal(20f);
                Debug.Log(actionItemType + " OK");
            }
            if (actionItemType.ToString() == "Mana")
            {
                Debug.Log(actionItemType + " OK");
            }
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}
