using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Core;
using System;

namespace Survivor.Combat{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] Weapon weapon = null;
        [SerializeField] Transform handTransform = null;
       
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        void Start()
        {
            SpawnWeapon();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            //remove for keyboard control
            if (target == null)
            {
                return;
            }
            if (target.IsDead())
            {
                return;
            }
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        void SpawnWeapon()
        {
            if(weapon == null)
            {
                return;
            }
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform,animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        //remove for keyboard control
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //animation event
        void Hit()
        {
            if(target == null)
            {
                return;
            }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }


     
    }    
}