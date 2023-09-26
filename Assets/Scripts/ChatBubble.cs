using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour {

    [SerializeField] private Transform pfChatBubble;

    
    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;
    
    public void Create(Transform parent, Vector3 localPosition, string text) {
        Transform chatBubbleTransform = Instantiate(pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);

        Destroy(chatBubbleTransform.gameObject, 6f);
    }

    private void Awake() {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    // private void Start() {
    //     Setup("Hello World!");
    // }

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