using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Core
{
    public class FollowCamera : MonoBehaviour
    {
      
        [SerializeField] Transform target;
       
       
       
        void Start()
        {
      
        }

        void LateUpdate()
        {
            transform.position = target.transform.position;
           
         
           
        }

    }
}
