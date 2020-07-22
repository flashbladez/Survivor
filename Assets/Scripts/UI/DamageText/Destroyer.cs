using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Survivor.UI
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;

        public void TargetToDestroy()
        {
            Destroy(targetToDestroy);
        }
    }
}
