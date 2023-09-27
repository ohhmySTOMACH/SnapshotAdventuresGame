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
        public void Interact()
        {
            ChatBubble.Create(transform.transform, new Vector3(posX, posY, posZ), "Hello there!");

            // mesh = transform.GetComponent<MeshCollider>();
            // if (mesh == null) {
            //     Debug.Log("Mesh is NUll");
            // } else {
            //     Debug.Log(mesh);
            // }
        }
    }
}


