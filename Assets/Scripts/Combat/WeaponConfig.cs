using Survivor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Attributes;
using GameDevTV.Inventories;
using Survivor.Stats;

namespace Survivor.Combat
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : EquipableItem,IModifierProvider
    {
        [SerializeField] Weapon equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float baseWeaponRange = 2f;
        [SerializeField] float baseWeaponDamage = 5f;
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] Projectile projectile = null;
        [SerializeField] bool isRightHanded = true;

        const string weaponName = "Weapon";

        [SerializeField]
        Modifier[] additveModifiers;
        [SerializeField]
        Modifier[] percentageModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
       
        public Weapon Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {          
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;        
            }
            return weapon;
        }

        void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null)
            {
                return;
            }
            oldWeapon.name = "Old Weapon";
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target,GameObject instigator,float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target,instigator,calculatedDamage);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

        public float GetDamage()
        {
            return baseWeaponDamage;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetRange()
        {
            return baseWeaponRange;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additveModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
            if (stat == Stat.Damage)
            {
                yield return baseWeaponDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
            if (stat == Stat.Damage)
            {
                yield return percentageBonus;
            }     
        }
    }
}
