using Survivor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Survivor.Saving;
using Survivor.Resources;

namespace Survivor.Movement{

    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
            
        NavMeshAgent navMeshAgent;
        Health health;

        void Awake()
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

        [Serializable]
        struct MoverSaveData
        {
           public SerializableVector3 position;
           public SerializableVector3 rotation;
        }


        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            //SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
