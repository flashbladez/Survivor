using Survivor.Combat;
using Survivor.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5; // max detection range to give chase
        Fighter fighter;
        GameObject player;
        Health health;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }


        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
           // GameObject player = GameObject.FindWithTag("Player");
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
               fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
