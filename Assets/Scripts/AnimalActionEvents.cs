using UnityEngine;
using Opsive.Shared.Events;

public class AnimalActionEvents : MonoBehaviour
{
    public void Awake()
    {
        EventHandler.RegisterEvent(gameObject, "OnPlayerCloseUp", OnPlayerCloseUp);
    }

    private void OnPlayerCloseUp()
    {
        AnimalAction animalAction = gameObject.GetComponent<AnimalAction>();
        animalAction.RunAway();
    }

    public void OnDestroy()
    {
        EventHandler.UnregisterEvent(gameObject, "OnPlayerCloseUp", OnPlayerCloseUp);
    }
}
