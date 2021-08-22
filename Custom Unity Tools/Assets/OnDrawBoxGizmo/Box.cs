using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnDrawGizmos()//Will not be showed on the game nether on the final product
    {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 2));

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
    }

    private void OnDrawGizmosSelected()//Will not be showed on the game nether on the final product
    {
        Gizmos.color = new Color(1,0,0,1);
        Gizmos.DrawWireCube(transform.position, new Vector3(2,2,2));

        Gizmos.color = new Color(1,0,0,0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
    }
}
