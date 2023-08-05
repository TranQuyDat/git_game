using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class waypointPath : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoint;
        [SerializeField] private bool active;
        public Transform[] wp => waypoint;
        private void OnDrawGizmos()
        {
            if (!active)
                return;
            Gizmos.color = Color.green;
            for(int i = 0; i < waypoint.Length-1; i++)
            {
                Transform from = waypoint[i];
                Transform to = waypoint[i + 1];

                Gizmos.DrawLine(from.position, to.position);

            }

            Gizmos.DrawLine(waypoint[0].position, waypoint[waypoint.Length-1].position);
        }

    }
}
