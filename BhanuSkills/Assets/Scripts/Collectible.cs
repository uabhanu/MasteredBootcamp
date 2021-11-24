using Events;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventsManager.InvokeEvent(BhanuSkillsEvent.CollectibleCollectedEvent);
        gameObject.SetActive(false);
    }
}
