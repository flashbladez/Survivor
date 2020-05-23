using Survivor.Combat;
using Survivor.Core;
using Survivor.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5; // max detection range to give chase
        [SerializeField] float suspicionTime = 5f;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPosition;
        Quaternion guardRotation;
        float timeSiceLastSawPlayer = Mathf.Infinity;
        
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
            guardRotation = transform.rotation;
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
                timeSiceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSiceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
               
            }
            timeSiceLastSawPlayer += Time.deltaTime;
        }
        void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        void SuspicionBehaviour()
        {
            transform.LookAt(player.transform.position);
            GetComponent<ActionScheduler>().CancelCurrentAction();//fighter.cancel() works too
        }

        void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
            //if (transform.position == guardPosition) {
            //    transform.rotation = guardRotation;
           // }
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
