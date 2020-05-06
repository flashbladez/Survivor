using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Camera
{
    public class FollowCamera : MonoBehaviour
    {
      
        [SerializeField] Transform target;
        [SerializeField] float offset = 10f;
       
       
        void Start()
        {
      
        }

        void LateUpdate()
        {
            transform.position = target.transform.position;
           
           // transform.position = new Vector3(target.position.x, target.position.y, target.position.z - offset);
           
        }

    }
}
