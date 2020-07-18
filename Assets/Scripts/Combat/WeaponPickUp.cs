using Survivor.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Survivor.Combat
{
    public class WeaponPickUp : MonoBehaviour,IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PickUp(other.GetComponent<Fighter>());
            }

        }

        void PickUp(Fighter fighter)
        {
           fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

        void ShowPickUp(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
           
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.GetComponent<Fighter>());
            }
            return true;
        }
    }
}
