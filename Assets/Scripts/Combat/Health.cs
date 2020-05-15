using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }


        void Update()
        {

        }
    }
}
