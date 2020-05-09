﻿using System.Collections;
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
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);
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
                if (Input.GetMouseButtonDown(0))//remove this for keyboard control
                {
                    GetComponent<Mover>().MoveTo(hit.point);
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
