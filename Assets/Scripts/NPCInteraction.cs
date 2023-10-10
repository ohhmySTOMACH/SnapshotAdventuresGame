using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace SnapshotChronicles.Interaction
{
    public class NPCInteraction : MonoBehaviour
    {
        // private MeshCollider mesh;
        [SerializeField] float posX = 0f;
        [SerializeField] float posY = 0f;
        [SerializeField] float posZ = 5f;
        public void ChatBubbleInteract()
        {
            ChatBubble.Create(transform.transform, new Vector3(posX, posY, posZ), "Hello there!");
        }

        public void DialogInteract()
        {
            Debug.Log("Player opened a conversation with NPC");
        }
    }
}


