using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class ChatBubble : MonoBehaviour {
    private static float localRotationX = 90f;
    private static float localRotationY = 0f;

    private static float localRotationZ = 0f;


    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    
    public static void Create(Transform parent, Vector3 localPosition, string text) {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;
        chatBubbleTransform.localRotation = Quaternion.Euler(localRotationX, localRotationY, localRotationZ);
        // chatBubbleTransform.LookAt(target);

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);

        Destroy(chatBubbleTransform.gameObject, 6f);
    }

    private void Awake() {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Setup(string text) {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(3f, 1f);
        backgroundSpriteRenderer.size = textSize + padding;

        // Vector3 offset = new Vector3(-3f, 0f);
        // backgroundSpriteRenderer.transform.localPosition = 
        //     new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;


        // TextWriter.AddWriter_Static(textMeshPro, text, .03f, true, true, () => { });
    }
}