﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Resources;
using Survivor.Control;

namespace Survivor.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
          
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }
            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}
