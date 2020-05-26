using Survivor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Survivor.Movement{

    public class Mover : MonoBehaviour,IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
            
        NavMeshAgent navMeshAgent;
        Health health;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
    
        void Update()
        {
            GetComponent<NavMeshAgent>().enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
          
            MoveTo(destination,speedFraction); //remove for keyboard control
        }

        public void MoveTo(Vector3 destination,float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            //remove first three lines for keyboard control
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
