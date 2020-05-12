using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Core;

namespace Survivor.Combat{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        private void Update()
        {

            //remove for keyboard control
            if(target == null)
            {
                return;
            }
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();

            }
        }

        private bool GetIsInRange()
        {
           // float distance = Vector3.Distance(transform.position, target.position);
          //  print("Distance" + distance + "   " + "Weapon Range " + weaponRange + (Vector3.Distance(transform.position, target.position) < weaponRange));
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        //remove for keyboard control
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}