using Survivor.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Combat
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] Projectile projectile = null;
        [SerializeField] bool isRightHanded = true;

        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            Transform handTransform = GetTransform(rightHand, leftHand);

            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }


            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target);
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
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}
