using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnapshotChronicles.Interaction
{
    public class NPCInteraction : MonoBehaviour
    {
        private ChatBubble chatBubble;
        public void Interact()
        {
            chatBubble = FindObjectOfType<ChatBubble>();
            if (chatBubble != null) {
                chatBubble.Create(transform.transform, new Vector3(-.3f,1.7f,0f), "Hello, There!");
            } else {
                Debug.Log("Chat Bubble is null");
            }
        }
    }
}


