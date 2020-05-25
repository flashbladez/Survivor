using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Combat;
using System;
using Survivor.Core;

namespace Survivor.Control{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            if (InteractWithCombat() )
            {
                return;
            }
            // click to move code
            if(InteractWithMovement())
            {
                return;
            }
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if(target == null)
                {
                    continue;
                }
                if(!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        // click to move code
        bool InteractWithMovement()
        {
            // click to move code
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))//remove this for keyboard control
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
