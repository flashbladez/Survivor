using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;

namespace Survivor.Combat{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        private void Update()
        {

            //remove for keyboard control
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (target != null && !isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        //remove for keyboard control
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
    }
}