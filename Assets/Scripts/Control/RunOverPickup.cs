using GameDevTV.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Survivor.Control
{
    [RequireComponent(typeof(Pickup))]
    public class RunOverPickup : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(other.gameObject == player)
            {
                GetComponent<Pickup>().PickupItem();
            }
        }
    }
}
