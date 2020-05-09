using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Survivor.Movement{

    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    
        void Update()
        {
        UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            //remove for keyboard control
            navMeshAgent.destination = destination;//remove for keyboard control
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            //remove for keyboard control
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
