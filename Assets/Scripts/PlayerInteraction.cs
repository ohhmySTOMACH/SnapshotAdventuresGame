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
        private DialogueManager dialogueManager;
        private const string PLAYER_CLOSE_TRIGGER = "PlayerCloseTrigger";
        private bool isInConversation = false;

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

        private void InteractWithNPC()
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.CompareTag("NPC"))
                { 
                    dialogueManager = GetComponent<DialogueManager>();
                    if (dialogueManager){
                        if (!isInConversation)
                        {
                            if (Input.GetKeyDown(KeyCode.F)) 
                            {
                                // npcInteraction = collider.GetComponent<NPCInteraction>();
                                // if (npcInteraction != null)
                                // {
                                //     // npcInteraction.ChatBubbleInteract();
                                //     npcInteraction.DialogInteract();
                                // }
                            
                                dialogueManager.StartDialogue(dialogue);
                                isInConversation = true;
                            }
                        } 
                        else 
                        {
                            if (Input.GetKeyDown(KeyCode.Return)) 
                            {
                                dialogueManager.DisplayNextSentence();
                                dialogueManager.OnEndDialogueCalled += HandleEndDialogue;
                            }
                        }
                    } else {
                        Debug.Log("dialogueManager object is not found");
                    }
                }
            }
        }

        private void HandleEndDialogue()
        {
            Debug.Log("EndDialogue is called");
            isInConversation = false;
        }
    }
}

