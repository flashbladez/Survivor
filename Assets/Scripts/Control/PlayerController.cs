using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Combat;
using System;

namespace Survivor.Control{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            InteractWithCombat();
            // click to move code
            InteractWithMovement();
        }

        void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null)
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        // click to move code
        void InteractWithMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveToCursor();
            }
        }

        // click to move code
        void MoveToCursor()
        {
            // click to move code
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
