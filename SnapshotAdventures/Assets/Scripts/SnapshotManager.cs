using UnityEngine;
using System.IO;

namespace Camera
{
    public class SnapshotManager : MonoBehaviour
    {
        private string screenshotDirectory;
        private string screenshotPath;
        private int snapshotNum = 0;

        void Start()
        {
            // TODO change String to constant
            screenshotDirectory = Application.persistentDataPath + "/Screenshots/";

            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            screenshotPath = screenshotDirectory + "myScreenshot" + snapshotNum + ".png";
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CaptureScreenshot();
                snapshotNum++;
            }
        }

        void CaptureScreenshot()
        {
            ScreenCapture.CaptureScreenshot(screenshotPath);
            Debug.Log("Screenshot saved to: " + screenshotPath);
        }
    }
}

