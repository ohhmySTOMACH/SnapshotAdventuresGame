using UnityEngine;
using Opsive.Shared.Events;

public class AnimalActionEvents : MonoBehaviour
{
    public void Awake()
    {
        EventHandler.RegisterEvent<GameObject>(gameObject, "OnPlayerCloseUp", OnPlayerCloseUp);
    }

    /// <summary>
    /// Receives the "OnPlayerCloseUp" event.
    /// </summary>
    private void OnPlayerCloseUp(GameObject player)
    {
        Debug.Log("The player closed up to" + gameObject);
        AnimalAction animalAction = gameObject.GetComponent<AnimalAction>();
        animalAction.RunAway();
        // TODO: Call the action perform method from animal object
    }

    public void OnDestroy()
    {
        // Unregister from the event when the component is no longer interested in it. In this example the component is interested for the lifetime of 
        // the component (Awake -> OnDestroy). 
        EventHandler.UnregisterEvent<GameObject>(gameObject, "OnPlayerCloseUp", OnPlayerCloseUp);
    }
}
