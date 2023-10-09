using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void OnDrawGizmosSelected()
    {
        Camera camera = GetComponent<Camera>();
        Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        // Debug.Log("camera.nearClipPlane: " + camera.nearClipPlane + " p: " + p);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p, 0.1f);
    }
    
}
