using System.Collections.Generic;
using UnityEngine;


public class CountPoints : MonoBehaviour
{
    public int rayMultiplier = 2;
    public const string TARGET_TAG = "Photograhpicable";
    public float maxRaycastDistance = 100f; 
    public bool showRayCastLine = true;
    public Color rayColor = Color.red;
    private HashSet<RaycastHit> photographicableObjectSet = new HashSet<RaycastHit>();

    public void CountPhotographicableObjectsInView()
    {
        
        int rayCountX = 16 * rayMultiplier;
        int rayCountY = 9 * rayMultiplier;

        // TODO: When click firebutton
        Camera mainCamera = Camera.main;

        // Calculate the dimensions of the view frustum
        float halfWidth = mainCamera.pixelWidth / 2f;
        float halfHeight = mainCamera.pixelHeight / 2f;

        for (int x = 0; x < rayCountX; x++)
        {
            for (int y = 0; y < rayCountY; y++)
            {
                // Calculate normalized screen coordinates (0 to 1)
                float normalizedX = (x + 0.5f) / rayCountX;
                float normalizedY = (y + 0.5f) / rayCountY;

                // Calculate ray origin (center of the pixel)
                Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(normalizedX, normalizedY, 0f));

                // Calculate ray direction (from camera position to ray origin)
                Vector3 rayDirection = rayOrigin - mainCamera.transform.position;

                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, rayDirection, out hit, maxRaycastDistance))
                {
                    if (showRayCastLine) {
                        Debug.Log("Hit point: " + hit.point);
                        Debug.DrawRay(mainCamera.transform.position, rayDirection, rayColor, 5.0f);
                    } else {
                        Debug.Log("Raycast line is not shown");
                    }

                    if (hit.collider.CompareTag(TARGET_TAG))
                    {
                        Debug.Log("Target Found");
                        photographicableObjectSet.Add(hit); 
                    } else {
                        Debug.Log("No Collider Tagged Named Animals, found: " + hit.collider.tag);
                    }
                }
            }
        }
        Debug.Log("Points for this picture: " + photographicableObjectSet.Count);
    }
}


