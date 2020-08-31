using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Movement;
using Survivor.Core;
using System;
using GameDevTV.Saving;
using Survivor.Attributes;
using Survivor.Stats;
using GameDevTV.Utils;
using GameDevTV.Inventories;

namespace Survivor.Combat{
    public class Fighter : MonoBehaviour, IAction,ISaveable
    {
       
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] WeaponConfig defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] HealthBar healthBar;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        WeaponConfig currentWeaponConfig = null;
        LazyValue<Weapon> currentWeapon;
        Equipment equipment;
       
       
        void Awake()
        {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetUpDefaultWeapon);
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        void Start()
        {
            currentWeapon.ForceInit();
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
            if (!GetIsInRange(target.transform))
            {
                GetComponent<Mover>().MoveTo(target.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        Weapon SetUpDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            if (weapon == null)
            {
                return;
            }
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;
            if(weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }

        }

        Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
           return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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
            if (!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform))
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
           // if (target != null)
           // {         
           //    healthBar.GetHealthRef(target);
          //  }
        }

        //animation event
        void Hit()
        {

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            if(currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }
            if (target == null)
            {
                return;
            }
            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
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

        bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetRange();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        } 
        
    }    
}