﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for(int i= 0;i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}