using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
