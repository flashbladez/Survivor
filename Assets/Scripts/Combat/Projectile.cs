using Survivor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;

        Health target = null;
        float damage = 0;

        void Start()
        {

        }

        void Update()
        {
            if(target == null)
            {
                return;
            }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() != target)
            {
                return;
            }
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
