using Survivor.Combat;
using Survivor.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Resources;
using Survivor.Core;
using Survivor.Utils;
using System;

namespace Survivor.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // max detection range to give chase
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 5f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] PatrolPath patrolPath;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        LazyValue<Vector3> guardPosition;
        Quaternion guardRotation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float randomMaxDwellTime = 0;
        float randomMinDwellTime = 0;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        void Start()
        {
            guardPosition.ForceInit();      
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
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();

        }

        void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        void SuspicionBehaviour()
        {
            transform.LookAt(player.transform.position);
            GetComponent<ActionScheduler>().CancelCurrentAction();//fighter.cancel() works too
        }

        void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            
            if (timeSinceArrivedAtWaypoint > randomMaxDwellTime)
            {
                 mover.StartMoveAction(nextPosition,patrolSpeedFraction);
            }
           
            if (transform.position.z == guardPosition.value.z)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, guardRotation, Time.deltaTime);
            }
        }
    
        bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        void CycleWaypoint()
        {
            randomMaxDwellTime = UnityEngine.Random.Range(randomMinDwellTime, waypointDwellTime);
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }


    }
}
