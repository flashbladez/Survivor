using Survivor.Combat;
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
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }


        void Update()
        {
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
    }
}
