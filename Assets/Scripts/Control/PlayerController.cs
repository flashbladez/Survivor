using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Movement;


namespace Survival.Control{
    public class PlayerController : MonoBehaviour
    {
        void Update(){
        // click to move code
            if (Input.GetMouseButtonDown(0))
            {
                MoveToCursor();
            }
        }
        
        // click to move code
        void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // click to move code
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
            GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    
    }
}
