using System.Collections.Generic;
using UnityEngine;


public class CountPoints : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public int rayMultiplier = 1;
    public const string TARGET_TAG = "Photographicable";
    public float maxRaycastDistance = 100f; 
    public bool showRayCastLine = true;
    public Color rayColor = Color.red;

    public void CountPhotographicableObjectsInView()
    {
        HashSet<RaycastHit> photographicableObjectSet = new HashSet<RaycastHit>();
        int rayCountX = 16 * rayMultiplier;
        int rayCountY = 9 * rayMultiplier;

        for (int x = 0; x < rayCountX; x++)
        {
            for (int y = 0; y < rayCountY; y++)
            {
                // Calculate normalized screen coordinates (0 to 1)
                float normalizedX = (x + 0.5f) / rayCountX;
                float normalizedY = (y + 0.5f) / rayCountY;

                // Calculate ray origin (center of the pixel)
                Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(normalizedX, normalizedY, maxRaycastDistance));

                // Calculate ray direction (from camera position to ray origin)
                Vector3 rayDirection = rayOrigin - mainCamera.transform.position;
                int layerMask = LayerMask.NameToLayer("Default");
                Debug.Log("Ray Direction: " + rayDirection + "Ray Origin: " + rayOrigin);


                if (showRayCastLine) {
                    Debug.DrawRay(mainCamera.transform.position, rayDirection * 10f, rayColor, 5.0f);
                } else {
                    Debug.Log("Raycast line is not shown");
                }

                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, rayDirection, out hit, maxRaycastDistance, layerMask)) //Ignore actual camera's layer
                {
                    Debug.Log("Hit point: " + hit.point);
                    if (hit.collider.CompareTag(TARGET_TAG))
                    {
                        Debug.Log("Target Found");
                        photographicableObjectSet.Add(hit); 
                    } else {
                        Debug.Log("No Collider Tagged Named Photographicable, found: " + hit.collider.tag + "Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                    }
                } else {
                    Debug.Log("No Raycast");
                }
            }
        }
        Debug.Log("Points for this picture: " + photographicableObjectSet.Count);
    }
}


