using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DisplayCollider : MonoBehaviour {
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}