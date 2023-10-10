using System;
using System.Collections.Generic;
using UnityEngine;


public class CountPoints : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public int rayMultiplier = 1;
    public const string TARGET_TAG = "Photographicable";
    public float maxRaycastDistance = 900f; 
    public bool showRayCastLine = true;
    public Color rayColor = Color.red;

    public void CountPhotographicableObjectsInView()
    {
        HashSet<String> photographicableObjectSet = new HashSet<String>();
        int rayCountX = 16 * rayMultiplier;
        int rayCountY = 9 * rayMultiplier;

        for (int x = 0; x < rayCountX; x++)
        {
            for (int y = 0; y < rayCountY; y++)
            {
                // Calculate normalized screen coordinates (0 to 1)
                float normalizedX = (x + 0.5f) / rayCountX;
                float normalizedY = (y + 0.5f) / rayCountY;

                Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(normalizedX, normalizedY, mainCamera.nearClipPlane));
                Vector3 rayEnd = mainCamera.ViewportToWorldPoint(new Vector3(normalizedX, normalizedY, maxRaycastDistance));

                Vector3 rayDirection = rayEnd - rayOrigin;
                int layerMask = LayerMask.NameToLayer("Default");

                if (showRayCastLine) {
                    Debug.DrawRay(rayOrigin, rayDirection * 10f, rayColor, 5.0f);
                }

                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRaycastDistance)) //Ignore actual camera's layer
                {
                    if (hit.collider.CompareTag(TARGET_TAG))
                    {
                        String hitColliderName = hit.collider.name;
                        // Debug.Log("Target Found, Name: " + hit.collider.name +", Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                        if (hitColliderName == "root"){
                            photographicableObjectSet.Add(hit.collider.transform.parent.name); 
                        } else {
                            photographicableObjectSet.Add(hitColliderName); 
                        }
                    }
                }
            }
        }
        Debug.Log("Points for this picture: " + photographicableObjectSet.Count);
        foreach (String name in photographicableObjectSet) {
            Debug.Log(name);
        }
    }
}


