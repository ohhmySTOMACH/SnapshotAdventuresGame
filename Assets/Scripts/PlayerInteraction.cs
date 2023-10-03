using System;
using System.Collections.Generic;
using UnityEngine;
using Opsive.Shared.Events;

namespace SnapshotChronicles.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        public List<GameObject> animalObjects = new List<GameObject>();
        [SerializeField] private float interactRange = 5f;
        private NPCInteraction npcInteraction;
        private const string PLAYER_CLOSE_TRIGGER = "PlayerCloseTrigger";
        void Update()
        {
            InteractWithNPC();
            foreach (GameObject animalObject in animalObjects) {
                float distanceToAnimal = Vector3.Distance(transform.position, animalObject.transform.position);
                if (distanceToAnimal <= interactRange) {
                    Opsive.Shared.Events.EventHandler.ExecuteEvent(animalObject, "OnPlayerCloseUp");
                }
            }
           

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
                            npcInteraction.Interact();
                        }
                    }
                }
            }
        }
    }
}

