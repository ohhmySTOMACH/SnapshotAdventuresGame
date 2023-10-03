using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAction : MonoBehaviour
{
    private const string PLAYER_CLOSE_TRIGGER= "PlayerCloseTrigger";
    public void RunAway() {
        GetComponent<Animator>().SetTrigger(PLAYER_CLOSE_TRIGGER);
    }
}
