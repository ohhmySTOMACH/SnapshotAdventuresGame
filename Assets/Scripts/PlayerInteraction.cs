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
        private const string PLAYER_CLOSE_TRIGGER = "PlayerCloseTrigger";
        void Update()
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) {
                if (Input.GetKeyDown(KeyCode.E)){
                    if (collider.CompareTag("NPC")) {
                        npcInteraction = collider.GetComponent<NPCInteraction>();
                        if (npcInteraction != null) 
                        {
                            npcInteraction.Interact();
                        }
                    }
                }
                
                if (collider.CompareTag("Animals")) {
                    collider.GetComponent<Animator>().SetTrigger(PLAYER_CLOSE_TRIGGER);
                }
            }
            
        }

    }
}

