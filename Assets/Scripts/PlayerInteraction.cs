using System;
using System.Collections.Generic;
using UnityEngine;
using Opsive.Shared.Events;

namespace SnapshotChronicles.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Dialogue dialogue;
        public List<GameObject> animalObjects = new List<GameObject>();
        [SerializeField] private float interactRange = 5f;
        private NPCInteraction npcInteraction;
        private const string PLAYER_CLOSE_TRIGGER = "PlayerCloseTrigger";
        void Update()
        {
            InteractWithNPC();
            // TODO: change to distance to triggered object.
            foreach (GameObject animalObject in animalObjects) {
                if (animalObject != null) {
                    float distanceToAnimal = Vector3.Distance(transform.position, animalObject.transform.position);
                    if (distanceToAnimal <= interactRange) {
                        Opsive.Shared.Events.EventHandler.ExecuteEvent(animalObject, "OnPlayerCloseUp");
                    }
                } else {
                    Debug.Log(animalObject.name + "is destroyed");
                }
            }
        }

        void FixedUpdate() {
            
        }

        private void InteractWithNPC()
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (collider.CompareTag("NPC"))
                    {
                        npcInteraction = collider.GetComponent<NPCInteraction>();
                        if (npcInteraction != null)
                        {
                            // npcInteraction.ChatBubbleInteract();
                            npcInteraction.DialogInteract();
                        }
                    }
                }
            }
        }
    }
}

