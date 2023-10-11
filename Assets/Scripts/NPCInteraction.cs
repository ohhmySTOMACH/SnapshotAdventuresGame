using UnityEngine;

namespace SnapshotChronicles.Interaction
{
    public class NPCInteraction : MonoBehaviour
    {
        public Dialogue dialogue;
        [SerializeField] private float posX = 0f;
        [SerializeField] private float posY = 0f;
        [SerializeField] private float posZ = 5f;
        
        DialogueManager dialogueManager;
        public void ChatBubbleInteract()
        {
            ChatBubble.Create(transform.transform, new Vector3(posX, posY, posZ), "Hello there!");
        }

        public void DialogInteract()
        {
            dialogueManager = GetComponent<DialogueManager>();
            if (dialogueManager) {
                dialogueManager.StartDialogue(dialogue);
            } else {
                Debug.Log("dialogueManager object is not found");
            }
        }
    }
}


