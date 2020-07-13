using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Core;
using System;
using Survivor.Saving;
using Survivor.Resources;
using Survivor.Stats;

namespace Survivor.Combat{
    public class Fighter : MonoBehaviour, IAction,ISaveable,IModifierProvider
    {
       
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
       
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
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

        public void EquipWeapon(Weapon weapon)
        {
            if(weapon == null)
            {
                return;
            }
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            currentWeapon = weapon;
        }

        public Health GetTarget()
        {
            return target;
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        void TriggerAttack()
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

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (target == null)
            {
                return;
            }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject,damage);
            }
          
        }

        void Shoot()
        {
            Hit();
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
           if (stat == Stat.Damage)
           {
                yield return currentWeapon.GetDamage();
           }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }
    }    
}