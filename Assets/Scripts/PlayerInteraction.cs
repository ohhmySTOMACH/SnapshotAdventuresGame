using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SnapshotChronicles.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactRange = 5f;
        private NPCInteraction npcInteraction;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)){
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray) {
                    npcInteraction = collider.GetComponent<NPCInteraction>();
                    if (npcInteraction != null) 
                    {
                        npcInteraction.Interact();
                    }
                }
            }
            
        }

    }
}

