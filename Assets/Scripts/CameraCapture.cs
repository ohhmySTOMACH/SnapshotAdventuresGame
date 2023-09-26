namespace SnapshotChronicles.Camera
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Threading;
    using UnityEngine;

    public class CameraCapture : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private Camera cameraCapture;

        [Header("Capture Settings")]
        [SerializeField] private int captureWidth = 1920;
        [SerializeField] private int captureHeight = 1080;
        [SerializeField] private AudioClip cameraShutterAudio;
        [SerializeField] private Material cameraShutterMaterial;

        [SerializeField] [Range(0f,0.1f)] float cameraShutterTime = 2e-3f; 
        
        private Transform cameraTransform;
        private Transform screenTransform;
        private Transform cameraLightTransform;

        private Light flashLight;
        private AudioSource m_audioSource;

        private Material cameraScreenMaterial;
        private MeshRenderer meshRendererCameraScreen;

        private Boolean isCapturing = false;
        private const String OUTPUT_TYPE = "png";
        private const String SCREEN_NAME = "screen";
        private const String LIGHT_NAME = "Light";

        private void Start() {
            m_audioSource = GetComponent<AudioSource>();
            cameraTransform = GetComponent<Transform>();
            screenTransform = cameraTransform.Find(SCREEN_NAME);
            cameraLightTransform = cameraTransform.Find(LIGHT_NAME); 

            if (screenTransform != null)
            {
                meshRendererCameraScreen = screenTransform.gameObject.GetComponent<MeshRenderer>();
                cameraScreenMaterial = new Material(meshRendererCameraScreen.material);
            } else {
                Debug.Log("screenTransform is null");
            }

            if (cameraLightTransform != null)
            {
                flashLight = cameraLightTransform.gameObject.GetComponent<Light>();
                flashLight.enabled = false;
            } else {
                Debug.Log("cameraLightTransform is null");
            }
        }
        
        public void OnCapture()
        {
            if (isCapturing) 
            {
                return;
            }
            isCapturing = true;
            StartCoroutine(Capture());
        }

        // Corouting for the capture
        private IEnumerator Capture()
        {
            ShutterAudioFeedback();
            StartCoroutine(ShutterVisualFeedback());

            RenderTexture oldCameraTargetTexture = cameraCapture.targetTexture;

            cameraCapture.targetTexture = RenderTexture.GetTemporary(captureWidth, captureHeight, 24, RenderTextureFormat.ARGB32);
            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = cameraCapture.targetTexture;
            cameraCapture.Render();

            Texture2D image = new Texture2D(captureWidth, captureHeight);
            image.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
            image.Apply();

            RenderTexture.active = activeRenderTexture;

            cameraCapture.targetTexture = oldCameraTargetTexture;
            yield return null;

            byte[] bytes = image.EncodeToPNG();
            Destroy(image);
            String path = Path.Combine(Application.persistentDataPath, GenerateUUID() + "." + OUTPUT_TYPE);
            File.WriteAllBytes(path, bytes);
            isCapturing = false;
            yield return null;
            Debug.Log("Capture done to " + path);
        }

         private IEnumerator ShutterVisualFeedback() {
            if (cameraLightTransform.gameObject.activeSelf) 
            {
                flashLight.enabled = true;
                yield return new WaitForSeconds(2*cameraShutterTime);
                flashLight.enabled = false;
            }
            yield return new WaitForSeconds(2*cameraShutterTime);
            meshRendererCameraScreen.material = cameraShutterMaterial;
            yield return new WaitForSeconds(cameraShutterTime);
            meshRendererCameraScreen.material = cameraScreenMaterial;
        }

        private void ShutterAudioFeedback()
        {
            m_audioSource.PlayOneShot(cameraShutterAudio);
        }

        private string GenerateUUID() {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}

