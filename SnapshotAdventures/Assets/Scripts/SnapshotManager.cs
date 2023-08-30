using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotManager : MonoBehaviour
{
    public List<Texture2D> cameraRoll = new List<Texture2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CapturePhotos();
        }
    }

    void CapturePhotos() 
    {
        Texture2D capturedScreenshot = ScreenCapture.CaptureScreenshotAsTexture();
        cameraRoll.Add(capturedScreenshot);
        Debug.Log("Photo added to camera roll.");
    }
}